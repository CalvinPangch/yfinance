using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces;

/// <summary>
/// Market-wide search and screening operations.
/// </summary>
public interface IMarketService
{
    Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
    Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default);
    Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default);
}
