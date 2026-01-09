using System.Globalization;
using System.Text.Json;
using YFinance.NET.Implementation.Constants;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for earnings and estimate data.
/// </summary>
public class EarningsScraper : IEarningsScraper
{
    private static readonly Dictionary<int, string> EventTypeMap = new()
    {
        [1] = "Call",
        [2] = "Earnings",
        [11] = "Meeting"
    };

    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    /// <summary>
    /// Initializes a new instance of the <see cref="EarningsScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    public EarningsScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    /// <summary>
    /// Gets earnings per share estimates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of periodic earnings estimates.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetEarningsEstimateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        return GetTrendEstimatesAsync(symbol, "earningsEstimate", cancellationToken);
    }

    /// <summary>
    /// Gets revenue estimates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of periodic revenue estimates.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetRevenueEstimateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        return GetTrendEstimatesAsync(symbol, "revenueEstimate", cancellationToken);
    }

    /// <summary>
    /// Gets historical earnings data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of historical earnings entries.</returns>
    public async Task<IReadOnlyList<EarningsHistoryEntry>> GetEarningsHistoryAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var result = await GetQuoteSummaryResultAsync(symbol, "earningsHistory", cancellationToken).ConfigureAwait(false);
        return result.HasValue ? ParseEarningsHistory(result.Value) : Array.Empty<EarningsHistoryEntry>();
    }

    /// <summary>
    /// Gets earnings per share trend data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of EPS trend entries.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetEpsTrendAsync(string symbol, CancellationToken cancellationToken = default)
    {
        return GetTrendEstimatesAsync(symbol, "epsTrend", cancellationToken);
    }

    /// <summary>
    /// Gets earnings per share revisions for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of EPS revisions.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetEpsRevisionsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        return GetTrendEstimatesAsync(symbol, "epsRevisions", cancellationToken);
    }

    /// <summary>
    /// Gets growth estimates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of growth estimate entries.</returns>
    public async Task<IReadOnlyList<GrowthEstimateEntry>> GetGrowthEstimatesAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var result = await GetQuoteSummaryResultAsync(
                symbol,
                "earningsTrend,industryTrend,sectorTrend,indexTrend",
                cancellationToken)
            .ConfigureAwait(false);

        return result.HasValue ? ParseGrowthEstimates(result.Value) : Array.Empty<GrowthEstimateEntry>();
    }

    /// <summary>
    /// Gets upcoming earnings dates matching the specified criteria.
    /// </summary>
    /// <param name="request">The earnings dates request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of earnings date entries.</returns>
    public async Task<IReadOnlyList<EarningsDateEntry>> GetEarningsDatesAsync(
        EarningsDatesRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        var endpoint = $"{YahooFinanceConstants.BaseUrls.Query1}/v1/finance/visualization";
        var payload = new Dictionary<string, object?>
        {
            ["size"] = request.Limit,
            ["offset"] = 0,
            ["sortField"] = "startdatetime",
            ["sortType"] = "DESC",
            ["entityIdType"] = "earnings",
            ["includeFields"] = new[]
            {
                "startdatetime",
                "eventstartdatetime",
                "eventtype",
                "timeZoneShortName",
                "epsestimate",
                "epsactual",
                "epssurprisepct"
            },
            ["query"] = new Dictionary<string, object?>
            {
                ["operator"] = "and",
                ["operands"] = new object[]
                {
                    new Dictionary<string, object?>
                    {
                        ["operator"] = "eq",
                        ["operands"] = new object[]
                        {
                            "ticker",
                            request.Symbol
                        }
                    }
                }
            }
        };

        var jsonBody = JsonSerializer.Serialize(payload);
        var jsonResponse = await _client.PostAsync(endpoint, jsonBody, cancellationToken).ConfigureAwait(false);
        return ParseEarningsDates(jsonResponse);
    }

    private async Task<IReadOnlyList<PeriodicEstimate>> GetTrendEstimatesAsync(
        string symbol,
        string estimateKey,
        CancellationToken cancellationToken)
    {
        var result = await GetQuoteSummaryResultAsync(symbol, "earningsTrend", cancellationToken).ConfigureAwait(false);
        return result.HasValue ? ParsePeriodicEstimates(result.Value, estimateKey) : Array.Empty<PeriodicEstimate>();
    }

    private async Task<JsonElement?> GetQuoteSummaryResultAsync(
        string symbol,
        string modules,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            ["modules"] = modules
        };

        var endpoint = string.Format(YahooFinanceConstants.Endpoints.QuoteSummary, symbol);
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);

        return TryGetQuoteSummaryResult(jsonResponse, out var result) ? result : null;
    }

    private static bool TryGetQuoteSummaryResult(string jsonResponse, out JsonElement result)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            result = default;
            return false;
        }

        result = results[0].Clone();
        return true;
    }

    private IReadOnlyList<PeriodicEstimate> ParsePeriodicEstimates(JsonElement result, string estimateKey)
    {
        if (!result.TryGetProperty("earningsTrend", out var earningsTrend) ||
            !earningsTrend.TryGetProperty("trend", out var trendArray) ||
            trendArray.ValueKind != JsonValueKind.Array)
        {
            return Array.Empty<PeriodicEstimate>();
        }

        var output = new List<PeriodicEstimate>();

        foreach (var trend in trendArray.EnumerateArray())
        {
            if (!trend.TryGetProperty(estimateKey, out var estimate) ||
                estimate.ValueKind == JsonValueKind.Null ||
                estimate.ValueKind == JsonValueKind.Undefined)
            {
                continue;
            }

            var period = trend.TryGetProperty("period", out var periodElement) && periodElement.ValueKind == JsonValueKind.String
                ? periodElement.GetString() ?? string.Empty
                : string.Empty;
            var metrics = ExtractMetrics(estimate);

            if (metrics.Count == 0 && string.IsNullOrWhiteSpace(period))
                continue;

            output.Add(new PeriodicEstimate
            {
                Period = period,
                Metrics = metrics
            });
        }

        return output;
    }

    private IReadOnlyList<EarningsHistoryEntry> ParseEarningsHistory(JsonElement result)
    {
        if (!result.TryGetProperty("earningsHistory", out var earningsHistory) ||
            !earningsHistory.TryGetProperty("history", out var historyArray) ||
            historyArray.ValueKind != JsonValueKind.Array)
        {
            return Array.Empty<EarningsHistoryEntry>();
        }

        var output = new List<EarningsHistoryEntry>();
        var skipKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "quarter"
        };

        foreach (var entry in historyArray.EnumerateArray())
        {
            DateTime? quarter = null;
            if (entry.TryGetProperty("quarter", out var quarterElement))
                quarter = ParseDateValue(quarterElement);

            var metrics = ExtractMetrics(entry, skipKeys);

            if (metrics.Count == 0 && !quarter.HasValue)
                continue;

            output.Add(new EarningsHistoryEntry
            {
                Quarter = quarter,
                Metrics = metrics
            });
        }

        return output;
    }

    private IReadOnlyList<GrowthEstimateEntry> ParseGrowthEstimates(JsonElement result)
    {
        var entries = new List<GrowthEstimateEntry>();
        var lookup = new Dictionary<string, GrowthEstimateEntry>(StringComparer.OrdinalIgnoreCase);

        void ApplyTrend(string moduleName, Action<GrowthEstimateEntry, decimal?> setter)
        {
            if (!result.TryGetProperty(moduleName, out var module) ||
                !module.TryGetProperty("trend", out var trends) ||
                trends.ValueKind != JsonValueKind.Array)
            {
                return;
            }

            foreach (var trend in trends.EnumerateArray())
            {
                var period = trend.TryGetProperty("period", out var periodElement) && periodElement.ValueKind == JsonValueKind.String
                    ? periodElement.GetString()
                    : null;
                if (string.IsNullOrWhiteSpace(period))
                    continue;

                if (!lookup.TryGetValue(period, out var entry))
                {
                    entry = new GrowthEstimateEntry { Period = period };
                    lookup[period] = entry;
                    entries.Add(entry);
                }

                var growth = trend.TryGetProperty("growth", out var growthElement)
                    ? _dataParser.ExtractDecimal(growthElement)
                    : null;

                setter(entry, growth);
            }
        }

        ApplyTrend("earningsTrend", (entry, value) => entry.StockTrend = value);
        ApplyTrend("industryTrend", (entry, value) => entry.IndustryTrend = value);
        ApplyTrend("sectorTrend", (entry, value) => entry.SectorTrend = value);
        ApplyTrend("indexTrend", (entry, value) => entry.IndexTrend = value);

        return entries;
    }

    private IReadOnlyList<EarningsDateEntry> ParseEarningsDates(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;
        var output = new List<EarningsDateEntry>();

        if (!root.TryGetProperty("finance", out var finance) ||
            !finance.TryGetProperty("result", out var resultArray) ||
            resultArray.ValueKind != JsonValueKind.Array ||
            resultArray.GetArrayLength() == 0)
        {
            return output;
        }

        var result = resultArray[0];
        var columns = GetColumnIndex(result);

        if (!result.TryGetProperty("rows", out var rows) ||
            rows.ValueKind != JsonValueKind.Array)
        {
            return output;
        }

        foreach (var row in rows.EnumerateArray())
        {
            var entry = new EarningsDateEntry
            {
                EarningsDate = GetDateValue(row, columns, "startdatetime", "startDateTime"),
                EventStartDate = GetDateValue(row, columns, "eventstartdatetime", "eventStartDateTime"),
                EventType = GetEventTypeValue(row, columns, "eventtype", "eventType"),
                TimeZoneShortName = GetStringValue(row, columns, "timeZoneShortName", "timezoneShortName"),
                EpsEstimate = GetDecimalValue(row, columns, "epsestimate", "epsEstimate"),
                ReportedEps = GetDecimalValue(row, columns, "epsactual", "epsActual"),
                SurprisePercent = GetDecimalValue(row, columns, "epssurprisepct", "epsSurprisePct")
            };

            if (entry.EarningsDate.HasValue || entry.EventStartDate.HasValue || entry.EpsEstimate.HasValue)
                output.Add(entry);
        }

        return output;
    }

    private static Dictionary<string, int> GetColumnIndex(JsonElement result)
    {
        var lookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        if (!result.TryGetProperty("columns", out var columns) ||
            columns.ValueKind != JsonValueKind.Array)
        {
            return lookup;
        }

        var index = 0;
        foreach (var column in columns.EnumerateArray())
        {
            if (column.TryGetProperty("field", out var field) && field.ValueKind == JsonValueKind.String)
            {
                var name = field.GetString();
                if (!string.IsNullOrWhiteSpace(name))
                    lookup[name] = index;
            }

            index++;
        }

        return lookup;
    }

    private DateTime? GetDateValue(JsonElement row, Dictionary<string, int> columns, params string[] fieldNames)
    {
        return TryGetRowValue(row, columns, out var value, fieldNames) ? ParseDateValue(value) : null;
    }

    private decimal? GetDecimalValue(JsonElement row, Dictionary<string, int> columns, params string[] fieldNames)
    {
        if (!TryGetRowValue(row, columns, out var value, fieldNames))
            return null;

        if (value.ValueKind == JsonValueKind.Object)
            return _dataParser.ExtractDecimal(value);

        if (value.ValueKind == JsonValueKind.Number)
            return value.GetDecimal();

        if (value.ValueKind == JsonValueKind.String &&
            decimal.TryParse(value.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        return null;
    }

    private string? GetStringValue(JsonElement row, Dictionary<string, int> columns, params string[] fieldNames)
    {
        if (!TryGetRowValue(row, columns, out var value, fieldNames))
            return null;

        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
    }

    private string? GetEventTypeValue(JsonElement row, Dictionary<string, int> columns, params string[] fieldNames)
    {
        if (!TryGetRowValue(row, columns, out var value, fieldNames))
            return null;

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var code))
        {
            return EventTypeMap.TryGetValue(code, out var label) ? label : code.ToString(CultureInfo.InvariantCulture);
        }

        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
    }

    private static bool TryGetRowValue(
        JsonElement row,
        Dictionary<string, int> columns,
        out JsonElement value,
        params string[] fieldNames)
    {
        if (row.ValueKind == JsonValueKind.Object)
        {
            foreach (var name in fieldNames)
            {
                if (row.TryGetProperty(name, out value))
                    return true;
            }
        }

        if (row.ValueKind == JsonValueKind.Array)
        {
            foreach (var name in fieldNames)
            {
                if (columns.TryGetValue(name, out var index) &&
                    row.GetArrayLength() > index)
                {
                    value = row[index];
                    return true;
                }
            }
        }

        value = default;
        return false;
    }

    private DateTime? ParseDateValue(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object &&
            element.TryGetProperty("raw", out var rawElement) &&
            rawElement.ValueKind == JsonValueKind.Number)
        {
            return _dataParser.UnixTimestampToDateTime(rawElement.GetInt64());
        }

        if (element.ValueKind == JsonValueKind.Number && element.TryGetInt64(out var unix))
            return _dataParser.UnixTimestampToDateTime(unix);

        if (element.ValueKind == JsonValueKind.String)
        {
            var raw = element.GetString();
            if (long.TryParse(raw, out var parsedUnix))
                return _dataParser.UnixTimestampToDateTime(parsedUnix);

            if (DateTime.TryParse(
                    raw,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var parsed))
            {
                return parsed;
            }
        }

        return null;
    }

    private Dictionary<string, decimal?> ExtractMetrics(JsonElement element, HashSet<string>? skipKeys = null)
    {
        var metrics = new Dictionary<string, decimal?>();
        var flat = _dataParser.FlattenResponse(element);

        foreach (var entry in flat)
        {
            if (skipKeys != null && skipKeys.Contains(entry.Key))
                continue;

            switch (entry.Value)
            {
                case decimal decValue:
                    metrics[entry.Key] = decValue;
                    break;
                case long longValue:
                    metrics[entry.Key] = longValue;
                    break;
                case int intValue:
                    metrics[entry.Key] = intValue;
                    break;
                case double doubleValue:
                    metrics[entry.Key] = (decimal)doubleValue;
                    break;
            }
        }

        return metrics;
    }
}
