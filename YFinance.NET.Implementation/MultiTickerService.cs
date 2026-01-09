using System.Collections.Concurrent;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation;

/// <summary>
/// Batch operations service for retrieving data for multiple tickers in parallel.
/// </summary>
public class MultiTickerService : IMultiTickerService
{
    private readonly IHistoryScraper _historyScraper;
    private readonly INewsScraper _newsScraper;
    private readonly ITickerService _tickerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MultiTickerService"/> class.
    /// </summary>
    /// <param name="historyScraper">Scraper for historical price data.</param>
    /// <param name="newsScraper">Scraper for news data.</param>
    /// <param name="tickerService">Main ticker service for various data types.</param>
    public MultiTickerService(IHistoryScraper historyScraper, INewsScraper newsScraper, ITickerService tickerService)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
        _newsScraper = newsScraper ?? throw new ArgumentNullException(nameof(newsScraper));
        _tickerService = tickerService ?? throw new ArgumentNullException(nameof(tickerService));
    }

    /// <summary>
    /// Gets historical price data for multiple symbols in parallel.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols.</param>
    /// <param name="request">The historical data request parameters.</param>
    /// <param name="maxConcurrency">Maximum number of concurrent requests. If null, uses processor count * 2.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping symbols to their historical data.</returns>
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
            symbol => _historyScraper.GetHistoryAsync(symbol, request, cancellationToken),
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

    /// <summary>
    /// Gets news for multiple symbols in parallel.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols.</param>
    /// <param name="count">Number of news items to retrieve per symbol.</param>
    /// <param name="maxConcurrency">Maximum number of concurrent requests. If null, uses processor count * 2.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping symbols to their news items.</returns>
    public async Task<Dictionary<string, IReadOnlyList<NewsItem>>> GetNewsAsync(
        IEnumerable<string> symbols,
        int count = 10,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);

        var symbolList = symbols.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct().ToList();
        var results = new ConcurrentDictionary<string, IReadOnlyList<NewsItem>>();

        if (symbolList.Count == 0)
            return new Dictionary<string, IReadOnlyList<NewsItem>>();

        var semaphore = new SemaphoreSlim(maxConcurrency ?? Math.Min(symbolList.Count, Environment.ProcessorCount * 2));
        var tasks = symbolList.Select(async symbol =>
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var request = new NewsRequest { Symbol = symbol, Count = count };
                var data = await _newsScraper.GetNewsAsync(request, cancellationToken).ConfigureAwait(false);
                results[symbol] = data;
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
        return results.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    /// Gets quote data for multiple symbols in parallel.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols.</param>
    /// <param name="maxConcurrency">Maximum number of concurrent requests. If null, uses processor count * 2.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping symbols to their quote data.</returns>
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

    /// <summary>
    /// Gets fast info data for multiple symbols in parallel.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols.</param>
    /// <param name="maxConcurrency">Maximum number of concurrent requests. If null, uses processor count * 2.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping symbols to their fast info data.</returns>
    public async Task<Dictionary<string, FastInfoData>> GetFastInfoAsync(
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

    /// <summary>
    /// Gets financial statements for multiple symbols in parallel.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols.</param>
    /// <param name="maxConcurrency">Maximum number of concurrent requests. If null, uses processor count * 2.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping symbols to their financial statements.</returns>
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

    /// <summary>
    /// Gets analyst data for multiple symbols in parallel.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols.</param>
    /// <param name="maxConcurrency">Maximum number of concurrent requests. If null, uses processor count * 2.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping symbols to their analyst data.</returns>
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
}
