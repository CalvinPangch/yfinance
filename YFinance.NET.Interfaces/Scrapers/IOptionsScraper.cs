using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving options data.
/// </summary>
public interface IOptionsScraper
{
    Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DateTime>> GetExpirationsAsync(string symbol, CancellationToken cancellationToken = default);
}
