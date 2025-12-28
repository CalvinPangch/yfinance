using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for shares outstanding and float history.
/// </summary>
public class SharesScraper : ISharesScraper
{
    private const string SharesOutstandingType = "sharesOutstanding";
    private const string FloatSharesType = "floatShares";

    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    public SharesScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    public async Task<SharesHistoryData> GetSharesHistoryAsync(
        SharesHistoryRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        var now = DateTimeOffset.UtcNow;
        var period1 = new DateTimeOffset(request.Start ?? now.AddYears(-2).UtcDateTime).ToUnixTimeSeconds().ToString();
        var period2 = new DateTimeOffset(request.End ?? now.UtcDateTime).ToUnixTimeSeconds().ToString();

        var endpoint = $"/ws/fundamentals-timeseries/v1/finance/timeseries/{request.Symbol}";
        var queryParams = new Dictionary<string, string>
        {
            { "type", $"{SharesOutstandingType},{FloatSharesType}" },
            { "period1", period1 },
            { "period2", period2 },
            { "merge", "false" }
        };

        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseSharesHistory(request.Symbol, jsonResponse);
    }

    private SharesHistoryData ParseSharesHistory(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("timeseries", out var timeseries) ||
            !timeseries.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return new SharesHistoryData { Symbol = symbol };
        }

        var result = results[0];
        var sharesEntries = ParseSeries(result, SharesOutstandingType);
        var floatEntries = ParseSeries(result, FloatSharesType);

        var entries = MergeEntries(sharesEntries, floatEntries);
        return new SharesHistoryData
        {
            Symbol = symbol,
            Entries = entries
        };
    }

    private Dictionary<DateTime, decimal?> ParseSeries(JsonElement result, string propertyName)
    {
        var output = new Dictionary<DateTime, decimal?>();
        if (!result.TryGetProperty(propertyName, out var series) ||
            series.ValueKind != JsonValueKind.Array)
        {
            return output;
        }

        foreach (var entry in series.EnumerateArray())
        {
            var date = GetDate(entry, "asOfDate");
            if (!date.HasValue)
                continue;

            var value = ExtractValue(entry);
            output[date.Value] = value;
        }

        return output;
    }

    private List<SharesHistoryEntry> MergeEntries(
        Dictionary<DateTime, decimal?> sharesEntries,
        Dictionary<DateTime, decimal?> floatEntries)
    {
        var dates = sharesEntries.Keys.Concat(floatEntries.Keys).Distinct().OrderBy(d => d).ToList();
        var entries = new List<SharesHistoryEntry>();

        foreach (var date in dates)
        {
            sharesEntries.TryGetValue(date, out var shares);
            floatEntries.TryGetValue(date, out var floatShares);

            entries.Add(new SharesHistoryEntry
            {
                Date = date,
                SharesOutstanding = shares,
                FloatShares = floatShares
            });
        }

        return entries;
    }

    private decimal? ExtractValue(JsonElement entry)
    {
        if (entry.TryGetProperty("reportedValue", out var reportedValue))
            return _dataParser.ExtractDecimal(reportedValue);
        if (entry.TryGetProperty("value", out var value))
            return _dataParser.ExtractDecimal(value);
        if (entry.TryGetProperty("raw", out var raw))
            return _dataParser.ExtractDecimal(raw);

        return null;
    }

    private DateTime? GetDate(JsonElement entry, string propertyName)
    {
        if (!entry.TryGetProperty(propertyName, out var value) || value.ValueKind != JsonValueKind.String)
            return null;

        if (DateTime.TryParse(value.GetString(), out var parsed))
            return DateTime.SpecifyKind(parsed, DateTimeKind.Utc);

        return null;
    }
}
