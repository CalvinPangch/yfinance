using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance lookup.
/// </summary>
public interface ILookupScraper
{
    Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default);
}
