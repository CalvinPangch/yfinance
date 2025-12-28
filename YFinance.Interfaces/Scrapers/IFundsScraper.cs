using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for ETF and mutual fund data.
/// </summary>
public interface IFundsScraper
{
    Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default);
}
