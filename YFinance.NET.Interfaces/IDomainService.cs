using YFinance.NET.Models;

namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for sector, industry, and market domain data.
/// </summary>
public interface IDomainService
{
    /// <summary>
    /// Gets sector data for the specified sector key.
    /// </summary>
    /// <param name="sectorKey">The sector key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sector data.</returns>
    Task<SectorData> GetSectorAsync(string sectorKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets industry data for the specified industry key.
    /// </summary>
    /// <param name="industryKey">The industry key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The industry data.</returns>
    Task<IndustryData> GetIndustryAsync(string industryKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets market data for the specified market.
    /// </summary>
    /// <param name="market">The market identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The market data.</returns>
    Task<MarketData> GetMarketAsync(string market, CancellationToken cancellationToken = default);
}
