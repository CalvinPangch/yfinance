using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance screeners.
/// </summary>
public interface IScreenerScraper
{
    Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default);
}
