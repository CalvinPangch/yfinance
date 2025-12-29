using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for detailed ticker info payload.
/// </summary>
public class InfoScraper : IInfoScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly ISymbolValidator _symbolValidator;

    public InfoScraper(IYahooFinanceClient client, ISymbolValidator symbolValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _symbolValidator = symbolValidator ?? throw new ArgumentNullException(nameof(symbolValidator));
    }

    public async Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var summaryParams = new Dictionary<string, string>
        {
            { "modules", "financialData,quoteType,defaultKeyStatistics,assetProfile,summaryDetail" }
        };

        var summaryEndpoint = $"/v10/finance/quoteSummary/{symbol}";
        var summaryResponse = await _client.GetAsync(summaryEndpoint, summaryParams, cancellationToken).ConfigureAwait(false);

        var query1Response = await _client.GetAsync(
            "/v7/finance/quote",
            new Dictionary<string, string> { { "symbols", symbol } },
            cancellationToken).ConfigureAwait(false);

        var values = MergeInfo(summaryResponse, query1Response);

        return new InfoData
        {
            Symbol = symbol,
            Values = values
        };
    }

    private static Dictionary<string, object?> MergeInfo(string summaryJson, string query1Json)
    {
        var merged = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        var summary = ExtractSummaryInfo(summaryJson);
        foreach (var (key, value) in summary)
            merged[key] = value;

        var query1 = ExtractQuery1Info(query1Json);
        foreach (var (key, value) in query1)
            merged[key] = value;

        if (merged.TryGetValue("maxAge", out var maxAge) && maxAge is long { } maxAgeValue && maxAgeValue == 1)
            merged["maxAge"] = 86400L;

        return merged;
    }

    private static Dictionary<string, object?> ExtractSummaryInfo(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        }

        return ConvertToDictionary(results[0]);
    }

    private static Dictionary<string, object?> ExtractQuery1Info(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteResponse", out var quoteResponse) ||
            !quoteResponse.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        }

        return ConvertToDictionary(results[0]);
    }

    private static Dictionary<string, object?> ConvertToDictionary(JsonElement element)
    {
        var result = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        if (element.ValueKind != JsonValueKind.Object)
            return result;

        foreach (var property in element.EnumerateObject())
        {
            result[property.Name] = ConvertElement(property.Name, property.Value);
        }

        return result;
    }

    private static object? ConvertElement(string name, JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            if (element.TryGetProperty("raw", out var raw))
                return ConvertElement(name, raw);

            var nested = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            foreach (var property in element.EnumerateObject())
            {
                nested[property.Name] = ConvertElement(property.Name, property.Value);
            }

            return nested;
        }

        if (element.ValueKind == JsonValueKind.Array)
        {
            var list = new List<object?>();
            foreach (var item in element.EnumerateArray())
            {
                list.Add(ConvertElement(name, item));
            }
            return list;
        }

        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.TryGetInt64(out var longValue) ? longValue : element.GetDecimal(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => null
        };
    }
}
