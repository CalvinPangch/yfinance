using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance visualization calendar endpoints.
/// </summary>
public interface ICalendarVisualizationScraper
{
    /// <summary>
    /// Retrieves calendar data from Yahoo Finance visualization endpoints.
    /// </summary>
    /// <param name="request">Calendar request with filters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar result with events matching the request criteria</returns>
    Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default);
}
