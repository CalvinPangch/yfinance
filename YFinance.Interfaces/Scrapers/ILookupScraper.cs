using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance lookup.
/// </summary>
public interface ILookupScraper
{
    Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default);
}
