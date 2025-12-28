using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for shares outstanding history.
/// </summary>
public interface ISharesScraper
{
    Task<SharesHistoryData> GetSharesHistoryAsync(SharesHistoryRequest request, CancellationToken cancellationToken = default);
}
