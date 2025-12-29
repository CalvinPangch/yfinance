using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for fast info snapshot.
/// </summary>
public interface IFastInfoScraper
{
    Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default);
}
