using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving institutional and insider holdings data.
/// </summary>
public interface IHoldersScraper
{
    /// <summary>
    /// Retrieves holder data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Major holders, institutional investors, and insider transactions</returns>
    Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default);
}
