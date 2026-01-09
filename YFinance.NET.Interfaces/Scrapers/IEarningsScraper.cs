using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper for earnings and estimate data.
/// </summary>
public interface IEarningsScraper
{
    /// <summary>
    /// Retrieves earnings estimates for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of earnings estimates by period</returns>
    Task<IReadOnlyList<PeriodicEstimate>> GetEarningsEstimateAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves revenue estimates for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of revenue estimates by period</returns>
    Task<IReadOnlyList<PeriodicEstimate>> GetRevenueEstimateAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves historical earnings data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of historical earnings entries</returns>
    Task<IReadOnlyList<EarningsHistoryEntry>> GetEarningsHistoryAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves EPS trend data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of EPS trends by period</returns>
    Task<IReadOnlyList<PeriodicEstimate>> GetEpsTrendAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves EPS revisions for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of EPS revisions by period</returns>
    Task<IReadOnlyList<PeriodicEstimate>> GetEpsRevisionsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves growth estimates for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of growth estimate entries</returns>
    Task<IReadOnlyList<GrowthEstimateEntry>> GetGrowthEstimatesAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves earnings dates for a specific ticker.
    /// </summary>
    /// <param name="request">Earnings dates request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of earnings date entries</returns>
    Task<IReadOnlyList<EarningsDateEntry>> GetEarningsDatesAsync(EarningsDatesRequest request, CancellationToken cancellationToken = default);
}
