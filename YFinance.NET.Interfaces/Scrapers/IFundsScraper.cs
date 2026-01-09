using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for ETF and mutual fund data.
/// </summary>
public interface IFundsScraper
{
    /// <summary>
    /// Gets fund-specific data for ETFs and mutual funds.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Fund data including holdings, sectors, and performance.</returns>
    Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default);
}
