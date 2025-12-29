using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance search.
/// </summary>
public interface ISearchScraper
{
    Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}
