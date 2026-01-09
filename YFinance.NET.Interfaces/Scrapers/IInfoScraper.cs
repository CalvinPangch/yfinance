using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for detailed info payload.
/// </summary>
public interface IInfoScraper
{
    /// <summary>
    /// Gets detailed information for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Comprehensive info data including company details, market data, and statistics.</returns>
    Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default);
}
