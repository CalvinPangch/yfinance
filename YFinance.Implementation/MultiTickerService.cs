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

    public MultiTickerService(IHistoryScraper historyScraper)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
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
}
