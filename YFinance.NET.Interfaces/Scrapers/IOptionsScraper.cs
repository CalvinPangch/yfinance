using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving options data.
/// </summary>
public interface IOptionsScraper
{
    /// <summary>
    /// Gets the option chain for the specified symbol and expiration date.
    /// </summary>
    /// <param name="request">The option chain request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Option chain data including calls and puts.</returns>
    Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all available expiration dates for options on the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of expiration dates.</returns>
    Task<IReadOnlyList<DateTime>> GetExpirationsAsync(string symbol, CancellationToken cancellationToken = default);
}
