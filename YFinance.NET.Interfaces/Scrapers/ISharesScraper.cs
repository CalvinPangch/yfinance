using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for shares outstanding history.
/// </summary>
public interface ISharesScraper
{
    /// <summary>
    /// Gets historical shares outstanding data for the specified symbol.
    /// </summary>
    /// <param name="request">The shares history request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Shares history data including timestamps and share counts.</returns>
    Task<SharesHistoryData> GetSharesHistoryAsync(SharesHistoryRequest request, CancellationToken cancellationToken = default);
}
