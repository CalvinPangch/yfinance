using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for ESG data.
/// </summary>
public interface IEsgScraper
{
    Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default);
}
