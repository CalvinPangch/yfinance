using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for analyst recommendations and price targets.
/// </summary>
public class AnalysisScraper : IAnalysisScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;
    private readonly ISymbolValidator _symbolValidator;

    public AnalysisScraper(IYahooFinanceClient client, IDataParser dataParser, ISymbolValidator symbolValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        _symbolValidator = symbolValidator ?? throw new ArgumentNullException(nameof(symbolValidator));
    }

    public async Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "recommendationTrend,financialData" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseAnalystData(symbol, jsonResponse);
    }

    public async Task<IReadOnlyList<RecommendationTrendEntry>> GetRecommendationsAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "recommendationTrend" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseRecommendationTrend(jsonResponse);
    }

    public async Task<RecommendationsSummaryData> GetRecommendationsSummaryAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "recommendationTrend" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseRecommendationsSummary(symbol, jsonResponse);
    }

    public async Task<IReadOnlyList<UpgradeDowngradeEntry>> GetUpgradesDowngradesAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "upgradeDowngradeHistory" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseUpgradeDowngradeHistory(jsonResponse);
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

    private IReadOnlyList<RecommendationTrendEntry> ParseRecommendationTrend(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return Array.Empty<RecommendationTrendEntry>();
        }

        var result = results[0];
        if (!result.TryGetProperty("recommendationTrend", out var trend) ||
            !trend.TryGetProperty("trend", out var trendArray) ||
            trendArray.ValueKind != JsonValueKind.Array)
        {
            return Array.Empty<RecommendationTrendEntry>();
        }

        var output = new List<RecommendationTrendEntry>();
        foreach (var entry in trendArray.EnumerateArray())
        {
            var period = entry.TryGetProperty("period", out var periodElement) && periodElement.ValueKind == JsonValueKind.String
                ? periodElement.GetString() ?? string.Empty
                : string.Empty;

            output.Add(new RecommendationTrendEntry
            {
                Period = period,
                StrongBuy = entry.GetIntPropertyOrDefault("strongBuy"),
                Buy = entry.GetIntPropertyOrDefault("buy"),
                Hold = entry.GetIntPropertyOrDefault("hold"),
                Sell = entry.GetIntPropertyOrDefault("sell"),
                StrongSell = entry.GetIntPropertyOrDefault("strongSell")
            });
        }

        return output;
    }

    private RecommendationsSummaryData ParseRecommendationsSummary(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        var summaryData = new RecommendationsSummaryData { Symbol = symbol };

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return summaryData;
        }

        var result = results[0];
        if (!result.TryGetProperty("recommendationTrend", out var trend) ||
            !trend.TryGetProperty("trend", out var trendArray) ||
            trendArray.ValueKind != JsonValueKind.Array)
        {
            return summaryData;
        }

        var summaries = new List<RecommendationsSummaryEntry>();
        foreach (var entry in trendArray.EnumerateArray())
        {
            var period = entry.TryGetProperty("period", out var periodElement) && periodElement.ValueKind == JsonValueKind.String
                ? periodElement.GetString() ?? string.Empty
                : string.Empty;

            summaries.Add(new RecommendationsSummaryEntry
            {
                Period = period,
                StrongBuy = entry.GetIntPropertyOrDefault("strongBuy"),
                Buy = entry.GetIntPropertyOrDefault("buy"),
                Hold = entry.GetIntPropertyOrDefault("hold"),
                Sell = entry.GetIntPropertyOrDefault("sell"),
                StrongSell = entry.GetIntPropertyOrDefault("strongSell")
            });
        }

        summaryData.Summaries = summaries;
        return summaryData;
    }

    private IReadOnlyList<UpgradeDowngradeEntry> ParseUpgradeDowngradeHistory(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return Array.Empty<UpgradeDowngradeEntry>();
        }

        var result = results[0];
        if (!result.TryGetProperty("upgradeDowngradeHistory", out var history) ||
            !history.TryGetProperty("history", out var historyArray) ||
            historyArray.ValueKind != JsonValueKind.Array)
        {
            return Array.Empty<UpgradeDowngradeEntry>();
        }

        var output = new List<UpgradeDowngradeEntry>();
        foreach (var entry in historyArray.EnumerateArray())
        {
            DateTime? gradeDate = null;
            if (entry.TryGetProperty("epochGradeDate", out var epoch) && epoch.ValueKind == JsonValueKind.Number)
                gradeDate = DateTimeOffset.FromUnixTimeSeconds(epoch.GetInt64()).UtcDateTime;

            output.Add(new UpgradeDowngradeEntry
            {
                GradeDate = gradeDate,
                Firm = entry.TryGetProperty("firm", out var firm) && firm.ValueKind == JsonValueKind.String ? firm.GetString() : null,
                ToGrade = entry.TryGetProperty("toGrade", out var toGrade) && toGrade.ValueKind == JsonValueKind.String ? toGrade.GetString() : null,
                FromGrade = entry.TryGetProperty("fromGrade", out var fromGrade) && fromGrade.ValueKind == JsonValueKind.String ? fromGrade.GetString() : null,
                Action = entry.TryGetProperty("action", out var action) && action.ValueKind == JsonValueKind.String ? action.GetString() : null
            });
        }

        return output;
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
