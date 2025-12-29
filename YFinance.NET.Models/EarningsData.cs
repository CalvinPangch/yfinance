namespace YFinance.NET.Models;

/// <summary>
/// Periodic estimate entry from earnings trend data.
/// </summary>
public class PeriodicEstimate
{
    public string Period { get; set; } = string.Empty;
    public Dictionary<string, decimal?> Metrics { get; set; } = new();
}

/// <summary>
/// Earnings history entry.
/// </summary>
public class EarningsHistoryEntry
{
    public DateTime? Quarter { get; set; }
    public Dictionary<string, decimal?> Metrics { get; set; } = new();
}

/// <summary>
/// Growth estimate entry by period.
/// </summary>
public class GrowthEstimateEntry
{
    public string Period { get; set; } = string.Empty;
    public decimal? StockTrend { get; set; }
    public decimal? IndustryTrend { get; set; }
    public decimal? SectorTrend { get; set; }
    public decimal? IndexTrend { get; set; }
}

/// <summary>
/// Earnings date entry.
/// </summary>
public class EarningsDateEntry
{
    public DateTime? EarningsDate { get; set; }
    public DateTime? EventStartDate { get; set; }
    public string? EventType { get; set; }
    public string? TimeZoneShortName { get; set; }
    public decimal? EpsEstimate { get; set; }
    public decimal? ReportedEps { get; set; }
    public decimal? SurprisePercent { get; set; }
}
