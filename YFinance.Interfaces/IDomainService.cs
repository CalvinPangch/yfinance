using YFinance.Models;

namespace YFinance.Interfaces;

/// <summary>
/// Service for sector, industry, and market domain data.
/// </summary>
public interface IDomainService
{
    Task<SectorData> GetSectorAsync(string sectorKey, CancellationToken cancellationToken = default);
    Task<IndustryData> GetIndustryAsync(string industryKey, CancellationToken cancellationToken = default);
    Task<MarketData> GetMarketAsync(string market, CancellationToken cancellationToken = default);
}
