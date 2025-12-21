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

    public TickerService(IHistoryScraper historyScraper)
    {
        _historyScraper = historyScraper;
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
        throw new NotImplementedException("Quote scraper not yet implemented");
    }

    public Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Fundamentals scraper not yet implemented");
    }

    public Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Analysis scraper not yet implemented");
    }

    public Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Holders scraper not yet implemented");
    }
}
