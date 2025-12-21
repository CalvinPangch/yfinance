using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces;

/// <summary>
/// Main service interface for ticker operations.
/// Provides access to all ticker-related data including quotes, history, financials, and analysis.
/// </summary>
public interface ITickerService
{
    /// <summary>
    /// Gets quote data for a specific ticker symbol.
    /// </summary>
    /// <param name="symbol">Ticker symbol (e.g., "AAPL")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Quote data including current price, market cap, P/E ratio, etc.</returns>
    Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets historical price data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="request">History request parameters (period, interval, start/end dates)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Historical OHLC data with dividends and splits</returns>
    Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets financial statement data (income statement, balance sheet, cash flow).
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Financial statement data</returns>
    Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets analyst recommendations and price targets.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Analyst data including recommendations and price targets</returns>
    Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets institutional and insider holdings data.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Holder data including major holders and institutional investors</returns>
    Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default);
}
