using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving historical price data.
/// Handles OHLC data, dividends, stock splits, and price repair.
/// </summary>
public interface IHistoryScraper
{
    /// <summary>
    /// Retrieves historical price data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="request">History request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Historical data with OHLC, volume, dividends, and splits</returns>
    Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default);
}
