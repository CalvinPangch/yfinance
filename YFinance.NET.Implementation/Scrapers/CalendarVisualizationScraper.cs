using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance visualization calendar endpoints.
/// </summary>
public class CalendarVisualizationScraper : ICalendarVisualizationScraper
{
    private readonly IYahooFinanceClient _client;

    private static readonly IReadOnlyDictionary<string, CalendarDefinition> Definitions =
        new Dictionary<string, CalendarDefinition>(StringComparer.OrdinalIgnoreCase)
        {
            ["sp_earnings"] = new CalendarDefinition(
                "intradaymarketcap",
                new[]
                {
                    "ticker", "companyshortname", "intradaymarketcap", "eventname", "startdatetime",
                    "startdatetimetype", "epsestimate", "epsactual", "epssurprisepct"
                },
                new[] { "Surprise (%)", "EPS Estimate", "Reported EPS" },
                new[] { "Event Start Date" },
                "Symbol",
                new Dictionary<string, string>
                {
                    ["Surprise (%)"] = "Surprise(%)",
                    ["Company Name"] = "Company",
                    ["Market Cap (Intraday)"] = "Marketcap"
                }),
            ["ipo_info"] = new CalendarDefinition(
                "startdatetime",
                new[]
                {
                    "ticker", "companyshortname", "exchange_short_name", "filingdate", "startdatetime",
                    "amendeddate", "pricefrom", "priceto", "offerprice", "currencyname", "shares", "dealtype"
                },
                new[] { "Price From", "Price To", "Price", "Shares" },
                new[] { "Filing Date", "Date", "Amended Date" },
                "Symbol",
                new Dictionary<string, string>
                {
                    ["Exchange Short Name"] = "Exchange"
                }),
            ["economic_event"] = new CalendarDefinition(
                "startdatetime",
                new[]
                {
                    "econ_release", "country_code", "startdatetime", "period", "after_release_actual",
                    "consensus_estimate", "prior_release_actual", "originally_reported_actual"
                },
                new[] { "Actual", "Market Expectation", "Prior to This", "Revised from" },
                new[] { "Event Time" },
                "Event",
                new Dictionary<string, string>
                {
                    ["Country Code"] = "Region",
                    ["Market Expectation"] = "Expected",
                    ["Prior to This"] = "Last",
                    ["Revised from"] = "Revised"
                }),
            ["splits"] = new CalendarDefinition(
                "startdatetime",
                new[]
                {
                    "ticker", "companyshortname", "startdatetime", "optionable", "old_share_worth", "share_worth"
                },
                Array.Empty<string>(),
                new[] { "Payable On" },
                "Symbol",
                new Dictionary<string, string>
                {
                    ["Optionable?"] = "Optionable"
                })
        };

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarVisualizationScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    public CalendarVisualizationScraper(IYahooFinanceClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Gets calendar data for the specified request.
    /// </summary>
    /// <param name="request">The calendar request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Calendar result containing structured data.</returns>
    public async Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!Definitions.TryGetValue(request.CalendarType, out var definition))
            throw new ArgumentException($"Unknown calendar type: {request.CalendarType}", nameof(request));

        var body = new Dictionary<string, object?>
        {
            ["sortType"] = "DESC",
            ["entityIdType"] = request.CalendarType,
            ["sortField"] = definition.SortField,
            ["includeFields"] = definition.IncludeFields,
            ["size"] = Math.Min(request.Limit, 100),
            ["offset"] = request.Offset,
            ["query"] = request.Query.ToDictionary()
        };

        var jsonBody = JsonSerializer.Serialize(body);
        var endpoint = "/v1/finance/visualization?lang=en-US&region=US";
        var jsonResponse = await _client.PostAsync(endpoint, jsonBody, cancellationToken).ConfigureAwait(false);

        return ParseCalendarResult(request.CalendarType, jsonResponse, definition);
    }

    private static CalendarResult ParseCalendarResult(string calendarType, string jsonResponse, CalendarDefinition definition)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (root.TryGetProperty("finance", out var finance) &&
            finance.TryGetProperty("error", out var error) &&
            error.ValueKind != JsonValueKind.Null &&
            error.ValueKind != JsonValueKind.Undefined)
        {
            throw new InvalidOperationException($"Calendar error: {error}");
        }

        var columns = new List<string>();
        var records = new List<Dictionary<string, object?>>();

        if (finance.TryGetProperty("result", out var resultArray) &&
            resultArray.ValueKind == JsonValueKind.Array &&
            resultArray.GetArrayLength() > 0)
        {
            var result = resultArray[0];
            if (result.TryGetProperty("documents", out var documents) &&
                documents.ValueKind == JsonValueKind.Array &&
                documents.GetArrayLength() > 0)
            {
                var doc = documents[0];
                if (doc.TryGetProperty("columns", out var columnElements) && columnElements.ValueKind == JsonValueKind.Array)
                {
                    foreach (var column in columnElements.EnumerateArray())
                    {
                        var label = column.TryGetProperty("label", out var labelElement) && labelElement.ValueKind == JsonValueKind.String
                            ? labelElement.GetString() ?? string.Empty
                            : string.Empty;
                        if (label == "Event Start Date" &&
                            column.TryGetProperty("type", out var typeElement) &&
                            typeElement.ValueKind == JsonValueKind.String &&
                            typeElement.GetString() == "STRING")
                        {
                            label = "Timing";
                        }

                        columns.Add(label);
                    }
                }

                if (doc.TryGetProperty("rows", out var rows) && rows.ValueKind == JsonValueKind.Array)
                {
                    foreach (var row in rows.EnumerateArray())
                    {
                        if (row.ValueKind != JsonValueKind.Array)
                            continue;

                        var record = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                        var values = row.EnumerateArray().ToList();
                        for (var i = 0; i < columns.Count && i < values.Count; i++)
                        {
                            record[columns[i]] = ConvertElement(values[i]);
                        }

                        records.Add(record);
                    }
                }
            }
        }

        ApplyCleanup(definition, records);

        return new CalendarResult
        {
            CalendarType = calendarType,
            Columns = columns,
            Records = records
        };
    }

    private static void ApplyCleanup(CalendarDefinition definition, List<Dictionary<string, object?>> records)
    {
        foreach (var record in records)
        {
            foreach (var nanColumn in definition.NaNColumns)
            {
                if (record.TryGetValue(nanColumn, out var value) && value is double doubleValue && doubleValue == 0d)
                    record[nanColumn] = null;
            }

            foreach (var rename in definition.Renames)
            {
                if (record.TryGetValue(rename.Key, out var value))
                {
                    record.Remove(rename.Key);
                    record[rename.Value] = value;
                }
            }

            foreach (var dateColumn in definition.DateColumns)
            {
                if (!record.TryGetValue(dateColumn, out var value) || value is not string dateValue)
                    continue;

                if (DateTime.TryParse(dateValue, out var parsed))
                    record[dateColumn] = parsed;
            }
        }
    }

    private static object? ConvertElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.TryGetInt64(out var longValue) ? longValue : element.GetDecimal(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => element.ToString()
        };
    }

    private sealed record CalendarDefinition(
        string SortField,
        string[] IncludeFields,
        string[] NaNColumns,
        string[] DateColumns,
        string IndexColumn,
        Dictionary<string, string> Renames);
}
