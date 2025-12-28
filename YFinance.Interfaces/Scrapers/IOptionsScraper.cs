using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving options data.
/// </summary>
public interface IOptionsScraper
{
    Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DateTime>> GetExpirationsAsync(string symbol, CancellationToken cancellationToken = default);
}
