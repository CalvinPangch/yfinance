using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation;

/// <summary>
/// Service wrapper for calendar visualization endpoints.
/// </summary>
public class CalendarService : ICalendarService
{
    private readonly ICalendarVisualizationScraper _scraper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarService"/> class.
    /// </summary>
    /// <param name="scraper">Calendar visualization scraper</param>
    public CalendarService(ICalendarVisualizationScraper scraper)
    {
        _scraper = scraper ?? throw new ArgumentNullException(nameof(scraper));
    }

    /// <summary>
    /// Retrieves calendar data based on the specified request.
    /// </summary>
    /// <param name="request">Calendar request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar result</returns>
    public Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _scraper.GetCalendarAsync(request, cancellationToken);
    }

    /// <summary>
    /// Retrieves earnings calendar events.
    /// </summary>
    /// <param name="start">Start date filter</param>
    /// <param name="end">End date filter</param>
    /// <param name="limit">Maximum number of results</param>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar result with earnings events</returns>
    public Task<CalendarResult> GetEarningsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default)
    {
        var (startDate, endDate) = NormalizeDateRange(start, end);
        var query = new CalendarQuery("and",
            new CalendarQuery("eq", "region", "us"),
            new CalendarQuery("or",
                new CalendarQuery("eq", "eventtype", "EAD"),
                new CalendarQuery("eq", "eventtype", "ERA")),
            new CalendarQuery("gte", "startdatetime", startDate),
            new CalendarQuery("lte", "startdatetime", endDate));

        var request = new CalendarRequest
        {
            CalendarType = "sp_earnings",
            Query = query,
            Limit = limit,
            Offset = offset
        };

        return _scraper.GetCalendarAsync(request, cancellationToken);
    }

    /// <summary>
    /// Retrieves IPO calendar events.
    /// </summary>
    /// <param name="start">Start date filter</param>
    /// <param name="end">End date filter</param>
    /// <param name="limit">Maximum number of results</param>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar result with IPO events</returns>
    public Task<CalendarResult> GetIpoCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default)
    {
        var (startDate, endDate) = NormalizeDateRange(start, end);
        var query = new CalendarQuery("or",
            new CalendarQuery("gtelt", "startdatetime", startDate, endDate),
            new CalendarQuery("gtelt", "filingdate", startDate, endDate),
            new CalendarQuery("gtelt", "amendeddate", startDate, endDate));

        var request = new CalendarRequest
        {
            CalendarType = "ipo_info",
            Query = query,
            Limit = limit,
            Offset = offset
        };

        return _scraper.GetCalendarAsync(request, cancellationToken);
    }

    /// <summary>
    /// Retrieves economic events calendar.
    /// </summary>
    /// <param name="start">Start date filter</param>
    /// <param name="end">End date filter</param>
    /// <param name="limit">Maximum number of results</param>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar result with economic events</returns>
    public Task<CalendarResult> GetEconomicEventsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default)
    {
        var (startDate, endDate) = NormalizeDateRange(start, end);
        var query = new CalendarQuery("and",
            new CalendarQuery("gte", "startdatetime", startDate),
            new CalendarQuery("lte", "startdatetime", endDate));

        var request = new CalendarRequest
        {
            CalendarType = "economic_event",
            Query = query,
            Limit = limit,
            Offset = offset
        };

        return _scraper.GetCalendarAsync(request, cancellationToken);
    }

    /// <summary>
    /// Retrieves stock splits calendar.
    /// </summary>
    /// <param name="start">Start date filter</param>
    /// <param name="end">End date filter</param>
    /// <param name="limit">Maximum number of results</param>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar result with stock split events</returns>
    public Task<CalendarResult> GetSplitsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default)
    {
        var (startDate, endDate) = NormalizeDateRange(start, end);
        var query = new CalendarQuery("and",
            new CalendarQuery("gte", "startdatetime", startDate),
            new CalendarQuery("lte", "startdatetime", endDate));

        var request = new CalendarRequest
        {
            CalendarType = "splits",
            Query = query,
            Limit = limit,
            Offset = offset
        };

        return _scraper.GetCalendarAsync(request, cancellationToken);
    }

    private static (string Start, string End) NormalizeDateRange(DateTime? start, DateTime? end)
    {
        var startDate = start ?? DateTime.UtcNow.Date;
        var endDate = end ?? startDate.AddDays(7);
        return (startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
    }
}
