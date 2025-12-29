using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance visualization calendar endpoints.
/// </summary>
public interface ICalendarVisualizationScraper
{
    Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default);
}
