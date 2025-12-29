using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces;

/// <summary>
/// Service for batch operations across multiple tickers.
/// </summary>
public interface IMultiTickerService
{
    /// <summary>
    /// Retrieves historical data for multiple tickers in parallel.
    /// </summary>
    /// <param name="symbols">Ticker symbols.</param>
    /// <param name="request">History request parameters.</param>
    /// <param name="maxConcurrency">Optional max parallelism.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary of symbol to historical data.</returns>
    Task<Dictionary<string, HistoricalData>> GetHistoryAsync(
        IEnumerable<string> symbols,
        HistoryRequest request,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves quote data for multiple tickers in parallel.
    /// </summary>
    /// <param name="symbols">Ticker symbols.</param>
    /// <param name="maxConcurrency">Optional max parallelism.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary of symbol to quote data.</returns>
    Task<Dictionary<string, QuoteData>> GetQuotesAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves fast info for multiple tickers in parallel.
    /// </summary>
    /// <param name="symbols">Ticker symbols.</param>
    /// <param name="maxConcurrency">Optional max parallelism.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary of symbol to fast info.</returns>
    Task<Dictionary<string, FastInfo>> GetFastInfoAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves financial statements for multiple tickers in parallel.
    /// </summary>
    /// <param name="symbols">Ticker symbols.</param>
    /// <param name="maxConcurrency">Optional max parallelism.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary of symbol to financial statement data.</returns>
    Task<Dictionary<string, FinancialStatement>> GetFinancialStatementsAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves analyst data for multiple tickers in parallel.
    /// </summary>
    /// <param name="symbols">Ticker symbols.</param>
    /// <param name="maxConcurrency">Optional max parallelism.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary of symbol to analyst data.</returns>
    Task<Dictionary<string, AnalystData>> GetAnalystDataAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default);
}
