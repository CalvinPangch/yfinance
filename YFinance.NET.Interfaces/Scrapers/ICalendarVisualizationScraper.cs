using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance visualization calendar endpoints.
/// </summary>
public interface ICalendarVisualizationScraper
{
    Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default);
}
