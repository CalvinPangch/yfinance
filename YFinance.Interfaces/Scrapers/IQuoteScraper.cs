using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving quote data from Yahoo Finance.
/// Fetches data from quoteSummary, query1, and timeseries endpoints.
/// </summary>
public interface IQuoteScraper
{
    /// <summary>
    /// Retrieves quote data for a specific ticker symbol.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Quote data including price, market cap, ratios, etc.</returns>
    Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default);
}
