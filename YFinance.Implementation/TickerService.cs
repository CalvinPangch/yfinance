using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Implementation;

/// <summary>
/// Main service for ticker operations.
/// Orchestrates calls to specialized scrapers for different data types.
/// </summary>
public class TickerService : ITickerService
{
    private readonly IHistoryScraper _historyScraper;
    private readonly IQuoteScraper _quoteScraper;
    private readonly IFundamentalsScraper _fundamentalsScraper;
    private readonly IAnalysisScraper _analysisScraper;
    private readonly IHoldersScraper _holdersScraper;

    public TickerService(
        IHistoryScraper historyScraper,
        IQuoteScraper quoteScraper,
        IFundamentalsScraper fundamentalsScraper,
        IAnalysisScraper analysisScraper,
        IHoldersScraper holdersScraper)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
        _quoteScraper = quoteScraper ?? throw new ArgumentNullException(nameof(quoteScraper));
        _fundamentalsScraper = fundamentalsScraper ?? throw new ArgumentNullException(nameof(fundamentalsScraper));
        _analysisScraper = analysisScraper ?? throw new ArgumentNullException(nameof(analysisScraper));
        _holdersScraper = holdersScraper ?? throw new ArgumentNullException(nameof(holdersScraper));
    }

    public Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return _historyScraper.GetHistoryAsync(symbol, request, cancellationToken);
    }

    public Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _quoteScraper.GetQuoteAsync(symbol, cancellationToken);
    }

    public Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fundamentalsScraper.GetFinancialStatementsAsync(symbol, cancellationToken);
    }

    public Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetAnalystDataAsync(symbol, cancellationToken);
    }

    public Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _holdersScraper.GetHoldersAsync(symbol, cancellationToken);
    }
}
