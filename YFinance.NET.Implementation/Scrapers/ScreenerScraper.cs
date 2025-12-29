using System.Text.Json;
using YFinance.NET.Implementation.Constants;
using YFinance.NET.Implementation.Utils;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance screeners.
/// </summary>
public class ScreenerScraper : IScreenerScraper
{
    private readonly IYahooFinanceClient _client;

    public ScreenerScraper(IYahooFinanceClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        if (!string.IsNullOrWhiteSpace(request.PredefinedId))
        {
            if (request.Offset.HasValue)
            {
                var predefined = ResolvePredefined(request.PredefinedId);
                var sortAsc = request.SortAsc ?? string.Equals(predefined.SortType, "ASC", StringComparison.OrdinalIgnoreCase);

                var customRequest = new ScreenerRequest
                {
                    Query = predefined.Query,
                    Offset = request.Offset ?? predefined.Offset,
                    Count = request.Count ?? predefined.Count,
                    SortField = request.SortField ?? predefined.SortField,
                    SortAsc = sortAsc,
                    UserId = request.UserId,
                    UserIdType = request.UserIdType
                };

                customRequest.Validate();
                return await ScreenCustomAsync(customRequest, cancellationToken).ConfigureAwait(false);
            }

            return await ScreenPredefinedAsync(request, cancellationToken).ConfigureAwait(false);
        }

        return await ScreenCustomAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private async Task<ScreenerResult> ScreenPredefinedAsync(ScreenerRequest request, CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["corsDomain"] = "finance.yahoo.com",
            ["formatted"] = "false",
            ["lang"] = "en-US",
            ["region"] = "US",
            ["scrIds"] = request.PredefinedId!
        };

        var count = request.Count ?? request.Size;
        if (count.HasValue)
            queryParams["count"] = count.Value.ToString();

        if (request.Offset.HasValue)
            queryParams["offset"] = request.Offset.Value.ToString();

        if (!string.IsNullOrWhiteSpace(request.SortField))
            queryParams["sortField"] = request.SortField;

        if (request.SortAsc.HasValue)
            queryParams["sortAsc"] = request.SortAsc.Value.ToString().ToLowerInvariant();

        if (!string.IsNullOrWhiteSpace(request.UserId))
            queryParams["userId"] = request.UserId!;

        if (!string.IsNullOrWhiteSpace(request.UserIdType))
            queryParams["userIdType"] = request.UserIdType!;

        var jsonResponse = await _client.GetAsync(YahooFinanceConstants.Endpoints.ScreenerPredefined, queryParams, cancellationToken)
            .ConfigureAwait(false);
        return ParseScreenerResult(jsonResponse);
    }

    private async Task<ScreenerResult> ScreenCustomAsync(ScreenerRequest request, CancellationToken cancellationToken)
    {
        var query = request.Query ?? throw new ArgumentException("Query is required for custom screener.");

        var body = new Dictionary<string, object?>
        {
            ["offset"] = request.Offset ?? 0,
            ["count"] = request.Count ?? 25,
            ["sortField"] = request.SortField ?? "ticker",
            ["sortType"] = (request.SortAsc ?? false) ? "ASC" : "DESC",
            ["userId"] = request.UserId ?? string.Empty,
            ["userIdType"] = request.UserIdType ?? "guid",
            ["query"] = query.ToDictionary()
        };

        if (request.Size.HasValue)
            body["size"] = request.Size.Value;

        if (query is EquityQuery)
            body["quoteType"] = "EQUITY";
        else if (query is FundQuery)
            body["quoteType"] = "MUTUALFUND";

        var jsonBody = JsonSerializer.Serialize(body);
        var jsonResponse = await _client.PostAsync(YahooFinanceConstants.Endpoints.Screener, jsonBody, cancellationToken)
            .ConfigureAwait(false);
        return ParseScreenerResult(jsonResponse);
    }

    private static ScreenerPredefinedQuery ResolvePredefined(string predefinedId)
    {
        if (!ScreenerPredefinedQueries.All.TryGetValue(predefinedId, out var predefined))
            throw new ArgumentException($"Unknown predefined screener '{predefinedId}'.", nameof(predefinedId));

        return predefined;
    }

    private static ScreenerResult ParseScreenerResult(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        var result = new ScreenerResult
        {
            RawResponse = jsonResponse
        };

        if (!root.TryGetProperty("finance", out var finance) ||
            !finance.TryGetProperty("result", out var resultArray) ||
            resultArray.ValueKind != JsonValueKind.Array ||
            resultArray.GetArrayLength() == 0)
        {
            return result;
        }

        var first = resultArray[0];
        result.Count = first.GetLongOrNull("count") is { } count ? (int?)count : null;
        result.Total = first.GetLongOrNull("total") is { } total ? (int?)total : null;

        if (first.TryGetProperty("quotes", out var quotes) &&
            quotes.ValueKind == JsonValueKind.Array)
        {
            foreach (var quote in quotes.EnumerateArray())
            {
                result.Quotes.Add(ParseQuote(quote));
            }
        }

        return result;
    }

    private static ScreenerQuote ParseQuote(JsonElement element)
    {
        return new ScreenerQuote
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
            RegularMarketVolume = element.GetLongOrNull("regularMarketVolume"),
            RawJson = element.GetRawText()
        };
    }
}
