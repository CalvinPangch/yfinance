using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance search.
/// </summary>
public interface ISearchScraper
{
    /// <summary>
    /// Searches for symbols matching the specified query.
    /// </summary>
    /// <param name="request">The search request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Search results containing matching symbols.</returns>
    Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}
