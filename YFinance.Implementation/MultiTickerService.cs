using System.Collections.Concurrent;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Implementation;

/// <summary>
/// Batch downloader for multi-ticker historical data.
/// </summary>
public class MultiTickerService : IMultiTickerService
{
    private readonly IHistoryScraper _historyScraper;
    private readonly INewsScraper _newsScraper;

    public MultiTickerService(IHistoryScraper historyScraper, INewsScraper newsScraper)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
        _newsScraper = newsScraper ?? throw new ArgumentNullException(nameof(newsScraper));
    }

    public async Task<Dictionary<string, HistoricalData>> GetHistoryAsync(
        IEnumerable<string> symbols,
        HistoryRequest request,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(symbols);
        ArgumentNullException.ThrowIfNull(request);

        var symbolList = symbols.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct().ToList();
        var results = new ConcurrentDictionary<string, HistoricalData>();

        if (symbolList.Count == 0)
            return new Dictionary<string, HistoricalData>();

        var semaphore = new SemaphoreSlim(maxConcurrency ?? Math.Min(symbolList.Count, Environment.ProcessorCount * 2));
        var tasks = symbolList.Select(async symbol =>
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var data = await _historyScraper.GetHistoryAsync(symbol, request, cancellationToken).ConfigureAwait(false);
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
}
