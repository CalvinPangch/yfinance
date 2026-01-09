using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance calendar data.
/// </summary>
public class CalendarScraper : ICalendarScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;
    private readonly ISymbolValidator _symbolValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    /// <param name="symbolValidator">The symbol validator for security.</param>
    public CalendarScraper(IYahooFinanceClient client, IDataParser dataParser, ISymbolValidator symbolValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        _symbolValidator = symbolValidator ?? throw new ArgumentNullException(nameof(symbolValidator));
    }

    /// <summary>
    /// Gets calendar events for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Calendar data for the symbol.</returns>
    public async Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "calendarEvents,summaryDetail" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseCalendar(symbol, jsonResponse);
    }

    private CalendarData ParseCalendar(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return new CalendarData { Symbol = symbol };
        }

        var result = results[0];
        var calendar = new CalendarData { Symbol = symbol };

        JsonElement? calendarEvents = null;
        if (result.TryGetProperty("calendarEvents", out var calendarEventsValue))
        {
            calendarEvents = calendarEventsValue;
            if (calendarEventsValue.TryGetProperty("earnings", out var earnings))
                ParseEarnings(calendar, earnings);

            calendar.DividendDate = ParseDate(calendarEventsValue, "dividendDate");
            calendar.ExDividendDate = ParseDate(calendarEventsValue, "exDividendDate");
            calendar.CapitalGainsDate = ParseDate(calendarEventsValue, "capitalGains");
        }

        if (result.TryGetProperty("summaryDetail", out var summaryDetail))
        {
            if (!calendar.ExDividendDate.HasValue)
                calendar.ExDividendDate = ParseDate(summaryDetail, "exDividendDate");

            if (summaryDetail.TryGetProperty("dividendRate", out var dividendRate))
                calendar.DividendAmount = _dataParser.ExtractDecimal(dividendRate);
        }

        if (calendarEvents.HasValue &&
            calendarEvents.Value.TryGetProperty("capitalGains", out var capitalGains))
        {
            calendar.CapitalGainsAmount = _dataParser.ExtractDecimal(capitalGains);
        }

        return calendar;
    }

    private void ParseEarnings(CalendarData calendar, JsonElement earnings)
    {
        if (earnings.TryGetProperty("earningsDate", out var earningsDateArray) &&
            earningsDateArray.ValueKind == JsonValueKind.Array)
        {
            calendar.EarningsDates = new List<DateTime>();
            foreach (var entry in earningsDateArray.EnumerateArray())
            {
                if (entry.TryGetProperty("raw", out var raw) && raw.ValueKind == JsonValueKind.Number)
                    calendar.EarningsDates.Add(DateTimeOffset.FromUnixTimeSeconds(raw.GetInt64()).UtcDateTime);
            }

            if (calendar.EarningsDates.Count == 0)
                calendar.EarningsDates = null;
        }

        calendar.EarningsAverage = ExtractDecimalFromProperty(earnings, "earningsAverage");
        calendar.EarningsLow = ExtractDecimalFromProperty(earnings, "earningsLow");
        calendar.EarningsHigh = ExtractDecimalFromProperty(earnings, "earningsHigh");
        calendar.RevenueAverage = ExtractDecimalFromProperty(earnings, "revenueAverage");
        calendar.RevenueLow = ExtractDecimalFromProperty(earnings, "revenueLow");
        calendar.RevenueHigh = ExtractDecimalFromProperty(earnings, "revenueHigh");
    }

    private decimal? ExtractDecimalFromProperty(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value) ? _dataParser.ExtractDecimal(value) : null;
    }

    private DateTime? ParseDate(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind == JsonValueKind.Null)
            return null;

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var unix))
            return DateTimeOffset.FromUnixTimeSeconds(unix).UtcDateTime;

        if (value.ValueKind == JsonValueKind.Object &&
            value.TryGetProperty("raw", out var raw) &&
            raw.ValueKind == JsonValueKind.Number &&
            raw.TryGetInt64(out var rawUnix))
        {
            return DateTimeOffset.FromUnixTimeSeconds(rawUnix).UtcDateTime;
        }

        return null;
    }
}
