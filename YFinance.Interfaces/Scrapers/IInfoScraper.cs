using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for detailed info payload.
/// </summary>
public interface IInfoScraper
{
    Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default);
}
