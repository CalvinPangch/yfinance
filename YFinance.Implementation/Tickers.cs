using YFinance.Interfaces;
using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Implementation;

/// <summary>
/// Convenient interface for managing multiple tickers.
/// Provides dictionary-like access to individual ticker services.
/// </summary>
public sealed class Tickers
{
    private readonly IMultiTickerService _multiTickerService;
    private readonly ITickerService _tickerService;
    private readonly Dictionary<string, string> _tickers;

    /// <summary>
    /// Gets the ticker symbols managed by this instance.
    /// </summary>
    public IReadOnlyList<string> Symbols => _tickers.Keys.ToList().AsReadOnly();

    /// <summary>
    /// Initializes a new instance of Tickers with space-separated symbols.
    /// </summary>
    /// <param name="symbols">Space-separated ticker symbols (e.g., "AAPL MSFT GOOGL")</param>
    /// <param name="tickerService">Ticker service for individual operations</param>
    /// <param name="multiTickerService">Multi-ticker service for batch operations</param>
    public Tickers(
        string symbols,
        ITickerService tickerService,
        IMultiTickerService multiTickerService)
    {
        ArgumentNullException.ThrowIfNull(symbols);
        ArgumentNullException.ThrowIfNull(tickerService);
        ArgumentNullException.ThrowIfNull(multiTickerService);

        _tickerService = tickerService;
        _multiTickerService = multiTickerService;

        // Parse space-separated symbols
        _tickers = symbols
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim().ToUpperInvariant())
            .Distinct()
            .ToDictionary(s => s, s => s);
    }

    /// <summary>
    /// Initializes a new instance of Tickers with a collection of symbols.
    /// </summary>
    /// <param name="symbols">Collection of ticker symbols</param>
    /// <param name="tickerService">Ticker service for individual operations</param>
    /// <param name="multiTickerService">Multi-ticker service for batch operations</param>
    public Tickers(
        IEnumerable<string> symbols,
        ITickerService tickerService,
        IMultiTickerService multiTickerService)
    {
        ArgumentNullException.ThrowIfNull(symbols);
        ArgumentNullException.ThrowIfNull(tickerService);
        ArgumentNullException.ThrowIfNull(multiTickerService);

        _tickerService = tickerService;
        _multiTickerService = multiTickerService;

        _tickers = symbols
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim().ToUpperInvariant())
            .Distinct()
            .ToDictionary(s => s, s => s);
    }

    /// <summary>
    /// Gets quote data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Quote data</returns>
    public Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default)
    {
        ValidateSymbol(symbol);
        return _tickerService.GetQuoteAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Downloads historical data for all tickers in parallel.
    /// </summary>
    /// <param name="request">History request parameters</param>
    /// <param name="maxConcurrency">Optional max concurrency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary of symbol to historical data</returns>
    public Task<Dictionary<string, HistoricalData>> DownloadAsync(
        HistoryRequest request,
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        return _multiTickerService.GetHistoryAsync(
            _tickers.Keys,
            request,
            maxConcurrency,
            cancellationToken);
    }

    /// <summary>
    /// Gets quote data for all tickers in parallel.
    /// </summary>
    /// <param name="maxConcurrency">Optional max concurrency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary of symbol to quote data</returns>
    public Task<Dictionary<string, QuoteData>> GetQuotesAsync(
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        return _multiTickerService.GetQuotesAsync(
            _tickers.Keys,
            maxConcurrency,
            cancellationToken);
    }

    /// <summary>
    /// Gets fast info for all tickers in parallel.
    /// </summary>
    /// <param name="maxConcurrency">Optional max concurrency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary of symbol to fast info</returns>
    public Task<Dictionary<string, FastInfo>> GetFastInfoAsync(
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        return _multiTickerService.GetFastInfoAsync(
            _tickers.Keys,
            maxConcurrency,
            cancellationToken);
    }

    /// <summary>
    /// Gets financial statements for all tickers in parallel.
    /// </summary>
    /// <param name="maxConcurrency">Optional max concurrency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary of symbol to financial statement data</returns>
    public Task<Dictionary<string, FinancialStatement>> GetFinancialStatementsAsync(
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        return _multiTickerService.GetFinancialStatementsAsync(
            _tickers.Keys,
            maxConcurrency,
            cancellationToken);
    }

    /// <summary>
    /// Gets analyst data for all tickers in parallel.
    /// </summary>
    /// <param name="maxConcurrency">Optional max concurrency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary of symbol to analyst data</returns>
    public Task<Dictionary<string, AnalystData>> GetAnalystDataAsync(
        int? maxConcurrency = null,
        CancellationToken cancellationToken = default)
    {
        return _multiTickerService.GetAnalystDataAsync(
            _tickers.Keys,
            maxConcurrency,
            cancellationToken);
    }

    private void ValidateSymbol(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        var normalizedSymbol = symbol.Trim().ToUpperInvariant();
        if (!_tickers.ContainsKey(normalizedSymbol))
            throw new ArgumentException($"Symbol '{symbol}' is not managed by this Tickers instance", nameof(symbol));
    }
}
