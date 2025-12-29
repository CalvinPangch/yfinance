using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for detailed info payload.
/// </summary>
public interface IInfoScraper
{
    Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default);
}
