using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for calendar events.
/// </summary>
public interface ICalendarScraper
{
    /// <summary>
    /// Retrieves calendar events for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Calendar data including earnings dates and dividends</returns>
    Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default);
}
