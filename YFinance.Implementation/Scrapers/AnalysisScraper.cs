using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Models;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for analyst recommendations and price targets.
/// </summary>
public class AnalysisScraper : IAnalysisScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    public AnalysisScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    public async Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "recommendationTrend,financialData" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseAnalystData(symbol, jsonResponse);
    }

    private AnalystData ParseAnalystData(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return new AnalystData { Symbol = symbol };
        }

        var result = results[0];
        var analystData = new AnalystData { Symbol = symbol };

        if (result.TryGetProperty("financialData", out var financialData))
        {
            analystData.TargetHighPrice = _dataParser.ExtractDecimal(financialData.GetPropertyOrDefault("targetHighPrice"));
            analystData.TargetLowPrice = _dataParser.ExtractDecimal(financialData.GetPropertyOrDefault("targetLowPrice"));
            analystData.TargetMeanPrice = _dataParser.ExtractDecimal(financialData.GetPropertyOrDefault("targetMeanPrice"));
            analystData.TargetMedianPrice = _dataParser.ExtractDecimal(financialData.GetPropertyOrDefault("targetMedianPrice"));

            if (financialData.TryGetProperty("numberOfAnalystOpinions", out var opinions))
                analystData.NumberOfAnalystOpinions = opinions.ValueKind == JsonValueKind.Number ? opinions.GetInt32() : null;
        }

        if (result.TryGetProperty("recommendationTrend", out var trend) &&
            trend.TryGetProperty("trend", out var trendArray) &&
            trendArray.ValueKind == JsonValueKind.Array &&
            trendArray.GetArrayLength() > 0)
        {
            var latest = trendArray[0];
            analystData.StrongBuy = latest.GetIntPropertyOrDefault("strongBuy");
            analystData.Buy = latest.GetIntPropertyOrDefault("buy");
            analystData.Hold = latest.GetIntPropertyOrDefault("hold");
            analystData.Sell = latest.GetIntPropertyOrDefault("sell");
            analystData.StrongSell = latest.GetIntPropertyOrDefault("strongSell");
        }

        return analystData;
    }
}

internal static class JsonElementExtensions
{
    public static JsonElement GetPropertyOrDefault(this JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value) ? value : default;
    }

    public static int? GetIntPropertyOrDefault(this JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var value) && value.ValueKind == JsonValueKind.Number)
            return value.GetInt32();

        return null;
    }
}
