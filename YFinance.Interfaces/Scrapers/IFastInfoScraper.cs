using YFinance.Models;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for fast info snapshot.
/// </summary>
public interface IFastInfoScraper
{
    Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default);
}
