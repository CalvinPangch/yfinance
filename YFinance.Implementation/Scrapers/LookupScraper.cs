using System.Text.Json;
using YFinance.Implementation.Constants;
using YFinance.Implementation.Utils;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Enums;
using YFinance.Models.Exceptions;
using YFinance.Models.Requests;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance lookup results.
/// </summary>
public class LookupScraper : ILookupScraper
{
    private readonly IYahooFinanceClient _client;
    public LookupScraper(IYahooFinanceClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        var queryParams = new Dictionary<string, string>
        {
            ["query"] = request.Query,
            ["type"] = ConvertLookupType(request.Type),
            ["start"] = "0",
            ["count"] = request.Count.ToString(),
            ["formatted"] = "false",
            ["fetchPricingData"] = request.FetchPricingData.ToString().ToLowerInvariant(),
            ["lang"] = request.Lang,
            ["region"] = request.Region
        };

        var jsonResponse = await _client.GetAsync(YahooFinanceConstants.Endpoints.Lookup, queryParams, cancellationToken)
            .ConfigureAwait(false);
        return ParseLookupResult(request.Query, jsonResponse);
    }

    private LookupResult ParseLookupResult(string query, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (root.TryGetProperty("finance", out var finance) &&
            finance.TryGetProperty("error", out var error) &&
            error.ValueKind != JsonValueKind.Null &&
            error.ValueKind != JsonValueKind.Undefined)
        {
            throw new YahooFinanceException($"Lookup returned error: {error.GetRawText()}");
        }

        var result = new LookupResult
        {
            Query = query,
            RawResponse = jsonResponse
        };

        if (!root.TryGetProperty("finance", out var financeRoot))
            return result;

        if (!financeRoot.TryGetProperty("result", out var resultArray) ||
            resultArray.ValueKind != JsonValueKind.Array ||
            resultArray.GetArrayLength() == 0)
        {
            return result;
        }

        var first = resultArray[0];
        if (!first.TryGetProperty("documents", out var documents) ||
            documents.ValueKind != JsonValueKind.Array)
        {
            return result;
        }

        foreach (var documentElement in documents.EnumerateArray())
        {
            result.Documents.Add(ParseDocument(documentElement));
        }

        return result;
    }

    private LookupDocument ParseDocument(JsonElement element)
    {
        return new LookupDocument
        {
            Symbol = element.GetStringOrNull("symbol"),
            ShortName = element.GetStringOrNull("shortName", "shortname"),
            LongName = element.GetStringOrNull("longName", "longname"),
            Exchange = element.GetStringOrNull("exchange"),
            QuoteType = element.GetStringOrNull("quoteType"),
            Region = element.GetStringOrNull("region"),
            Currency = element.GetStringOrNull("currency"),
            RegularMarketPrice = element.GetDecimalOrNull("regularMarketPrice"),
            RegularMarketChangePercent = element.GetDecimalOrNull("regularMarketChangePercent"),
            MarketCap = element.GetDecimalOrNull("marketCap"),
            RawJson = element.GetRawText()
        };
    }

    private static string ConvertLookupType(LookupType type)
    {
        return type switch
        {
            LookupType.All => "all",
            LookupType.Equity => "equity",
            LookupType.MutualFund => "mutualfund",
            LookupType.Etf => "etf",
            LookupType.Index => "index",
            LookupType.Future => "future",
            LookupType.Currency => "currency",
            LookupType.Cryptocurrency => "cryptocurrency",
            _ => "all"
        };
    }
}
