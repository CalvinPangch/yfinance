using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance search.
/// </summary>
public interface ISearchScraper
{
    Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}
