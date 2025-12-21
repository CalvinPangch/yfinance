using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving analyst recommendations and price targets.
/// </summary>
public interface IAnalysisScraper
{
    /// <summary>
    /// Retrieves analyst data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Analyst recommendations, price targets, and upgrades/downgrades</returns>
    Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default);
}
