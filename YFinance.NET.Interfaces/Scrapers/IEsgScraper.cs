using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for ESG data.
/// </summary>
public interface IEsgScraper
{
    /// <summary>
    /// Retrieves ESG (Environmental, Social, Governance) data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ESG data including scores and ratings</returns>
    Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default);
}
