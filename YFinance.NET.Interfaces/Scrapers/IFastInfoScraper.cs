using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for fast info snapshot.
/// </summary>
public interface IFastInfoScraper
{
    /// <summary>
    /// Gets fast info data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Fast info data containing key metrics.</returns>
    Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default);
}
