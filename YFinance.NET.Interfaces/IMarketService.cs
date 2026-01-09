using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces;

/// <summary>
/// Market-wide search and screening operations.
/// </summary>
public interface IMarketService
{
    /// <summary>
    /// Searches for symbols matching the specified criteria.
    /// </summary>
    /// <param name="request">The search request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The search results.</returns>
    Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Looks up symbols by various identifiers.
    /// </summary>
    /// <param name="request">The lookup request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The lookup results.</returns>
    Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Screens stocks based on specified criteria.
    /// </summary>
    /// <param name="request">The screener request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The screener results.</returns>
    Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default);
}
