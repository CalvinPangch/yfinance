using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

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

    /// <summary>
    /// Retrieves historical recommendation trends for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of recommendation trends over time</returns>
    Task<IReadOnlyList<RecommendationTrendEntry>> GetRecommendationsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves aggregated recommendations summary by time period.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Recommendations summary with period-based aggregation</returns>
    Task<RecommendationsSummaryData> GetRecommendationsSummaryAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves analyst upgrades and downgrades for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of upgrade and downgrade events</returns>
    Task<IReadOnlyList<UpgradeDowngradeEntry>> GetUpgradesDowngradesAsync(string symbol, CancellationToken cancellationToken = default);
}
