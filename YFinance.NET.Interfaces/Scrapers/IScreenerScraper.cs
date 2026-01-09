using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance screeners.
/// </summary>
public interface IScreenerScraper
{
    /// <summary>
    /// Performs a stock screening based on the specified criteria.
    /// </summary>
    /// <param name="request">The screener request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Screener results containing matching symbols.</returns>
    Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default);
}
