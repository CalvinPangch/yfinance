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

    /// <summary>
    /// Initializes a new instance of the <see cref="MarketService"/> class.
    /// </summary>
    /// <param name="searchScraper">Scraper for market search operations.</param>
    /// <param name="lookupScraper">Scraper for symbol lookup operations.</param>
    /// <param name="screenerScraper">Scraper for market screening operations.</param>
    public MarketService(
        ISearchScraper searchScraper,
        ILookupScraper lookupScraper,
        IScreenerScraper screenerScraper)
    {
        _searchScraper = searchScraper ?? throw new ArgumentNullException(nameof(searchScraper));
        _lookupScraper = lookupScraper ?? throw new ArgumentNullException(nameof(lookupScraper));
        _screenerScraper = screenerScraper ?? throw new ArgumentNullException(nameof(screenerScraper));
    }

    /// <summary>
    /// Searches for symbols in the market based on search criteria.
    /// </summary>
    /// <param name="request">The search request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The search results.</returns>
    public Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _searchScraper.SearchAsync(request, cancellationToken);
    }

    /// <summary>
    /// Looks up symbols matching the specified criteria.
    /// </summary>
    /// <param name="request">The lookup request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The lookup results.</returns>
    public Task<LookupResult> LookupAsync(LookupRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _lookupScraper.LookupAsync(request, cancellationToken);
    }

    /// <summary>
    /// Screens the market based on specified criteria.
    /// </summary>
    /// <param name="request">The screening request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The screening results.</returns>
    public Task<ScreenerResult> ScreenAsync(ScreenerRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _screenerScraper.ScreenAsync(request, cancellationToken);
    }
}
