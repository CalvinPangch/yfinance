using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation;

/// <summary>
/// Market-wide search and screening operations.
/// </summary>
public class MarketService : IMarketService
{
    private readonly ISearchScraper _searchScraper;
    private readonly ILookupScraper _lookupScraper;
    private readonly IScreenerScraper _screenerScraper;

    public MarketService(
        ISearchScraper searchScraper,
        ILookupScraper lookupScraper,
        IScreenerScraper screenerScraper)
    {
        _searchScraper = searchScraper ?? throw new ArgumentNullException(nameof(searchScraper));
        _lookupScraper = lookupScraper ?? throw new ArgumentNullException(nameof(lookupScraper));
        _screenerScraper = screenerScraper ?? throw new ArgumentNullException(nameof(screenerScraper));
    }

    public Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _searchScraper.SearchAsync(request, cancellationToken);
    }

    public Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _lookupScraper.LookupAsync(request, cancellationToken);
    }

    public Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _screenerScraper.ScreenAsync(request, cancellationToken);
    }
}
