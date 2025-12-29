using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

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

    /// <summary>
    /// Retrieves chart metadata for a ticker symbol.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>History metadata including trading periods and timezone</returns>
    Task<HistoryMetadata> GetHistoryMetadataAsync(string symbol, CancellationToken cancellationToken = default);
}
