using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces;

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
    /// Retrieves news for multiple tickers in parallel.
    /// </summary>
    /// <param name="symbols">Ticker symbols.</param>
    /// <param name="count">Number of news items per ticker.</param>
    /// <param name="maxConcurrency">Optional max parallelism.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary of symbol to news items.</returns>
    Task<Dictionary<string, IReadOnlyList<NewsItem>>> GetNewsAsync(
        IEnumerable<string> symbols,
        int count = 10,
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
    Task<Dictionary<string, FastInfoData>> GetFastInfoAsync(
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
