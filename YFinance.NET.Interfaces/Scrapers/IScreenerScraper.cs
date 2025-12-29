using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance screeners.
/// </summary>
public interface IScreenerScraper
{
    Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default);
}
