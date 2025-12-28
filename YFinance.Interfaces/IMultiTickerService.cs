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
}
