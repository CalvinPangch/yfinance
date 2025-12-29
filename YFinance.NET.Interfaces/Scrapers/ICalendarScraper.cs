using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for calendar events.
/// </summary>
public interface ICalendarScraper
{
    Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default);
}
