using System.Collections.Concurrent;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Implementation;

/// <summary>
/// Batch operations service for retrieving data for multiple tickers in parallel.
/// </summary>
public class MultiTickerService : IMultiTickerService
{
    private readonly ITickerService _tickerService;

    public MultiTickerService(ITickerService tickerService)
    {
        _tickerService = tickerService ?? throw new ArgumentNullException(nameof(tickerService));
    }

    public async Task<Dictionary<string, HistoricalData>> GetHistoryAsync(
        IEnumerable<string> symbols,
        HistoryRequest request,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);
        ArgumentNullException.ThrowIfNull(request);

        return await FetchBatchAsync(
            symbols,
            symbol => _tickerService.GetHistoryAsync(symbol, request, cancellationToken),
            maxConcurrency,
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<Dictionary<string, QuoteData>> GetQuotesAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);

        return await FetchBatchAsync(
            symbols,
            symbol => _tickerService.GetQuoteAsync(symbol, cancellationToken),
            maxConcurrency,
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<Dictionary<string, FastInfo>> GetFastInfoAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);

        return await FetchBatchAsync(
            symbols,
            symbol => _tickerService.GetFastInfoAsync(symbol, cancellationToken),
            maxConcurrency,
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<Dictionary<string, FinancialStatement>> GetFinancialStatementsAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);

        return await FetchBatchAsync(
            symbols,
            symbol => _tickerService.GetFinancialStatementsAsync(symbol, cancellationToken),
            maxConcurrency,
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<Dictionary<string, AnalystData>> GetAnalystDataAsync(
        IEnumerable<string> symbols,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);

        return await FetchBatchAsync(
            symbols,
            symbol => _tickerService.GetAnalystDataAsync(symbol, cancellationToken),
            maxConcurrency,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Generic batch fetcher with concurrency control and error handling.
    /// </summary>
    private static async Task<Dictionary<string, T>> FetchBatchAsync<T>(
        IEnumerable<string> symbols,
        Func<string, Task<T>> fetchFunc,
        int? maxConcurrency,
        CancellationToken cancellationToken)
    {
        var symbolList = symbols.Where(s => !string.IsNullOrWhiteSpace(s))
                                .Select(s => s.Trim())
                                .Distinct()
                                .ToList();

        if (symbolList.Count == 0)
            return new Dictionary<string, T>();

        var results = new ConcurrentDictionary<string, T>();
        var concurrency = maxConcurrency ?? Math.Min(symbolList.Count, Environment.ProcessorCount * 2);
        var semaphore = new SemaphoreSlim(concurrency);

        var tasks = symbolList.Select(async symbol =>
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var data = await fetchFunc(symbol).ConfigureAwait(false);
                results[symbol] = data;
            }
            catch
            {
                // Log error but continue with other symbols
                // Errors can be detected by checking if symbol is in results dictionary
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
        return results.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
