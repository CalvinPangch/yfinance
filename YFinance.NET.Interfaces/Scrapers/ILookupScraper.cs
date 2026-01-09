using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance lookup.
/// </summary>
public interface ILookupScraper
{
    /// <summary>
    /// Looks up symbols based on the specified criteria.
    /// </summary>
    /// <param name="request">The lookup request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Lookup results matching the search criteria.</returns>
    Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default);
}
