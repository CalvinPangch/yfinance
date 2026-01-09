using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for Yahoo Finance news.
/// </summary>
public interface INewsScraper
{
    /// <summary>
    /// Gets news articles for the specified symbols or criteria.
    /// </summary>
    /// <param name="request">The news request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of news items.</returns>
    Task<IReadOnlyList<NewsItem>> GetNewsAsync(NewsRequest request, CancellationToken cancellationToken = default);
}
