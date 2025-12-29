namespace YFinance.Models;

public class DomainOverview
{
    public int? CompaniesCount { get; set; }
    public decimal? MarketCap { get; set; }
    public string? MessageBoardId { get; set; }
    public string? Description { get; set; }
    public int? IndustriesCount { get; set; }
    public decimal? MarketWeight { get; set; }
    public long? EmployeeCount { get; set; }
}

public class DomainCompany
{
    public string Symbol { get; set; } = string.Empty;
    public string? Name { get; set; }
    public decimal? Rating { get; set; }
    public decimal? MarketWeight { get; set; }
}

public class DomainResearchReport
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Publisher { get; set; }
}

public class SectorData
{
    public string Key { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public DomainOverview? Overview { get; set; }
    public List<DomainCompany> TopCompanies { get; set; } = new();
    public Dictionary<string, string> TopEtfs { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> TopMutualFunds { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public List<IndustryEntry> Industries { get; set; } = new();
    public List<DomainResearchReport> ResearchReports { get; set; } = new();
}

public class IndustryEntry
{
    public string Key { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public decimal? MarketWeight { get; set; }
}

public class IndustryData
{
    public string Key { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public string? SectorKey { get; set; }
    public string? SectorName { get; set; }
    public DomainOverview? Overview { get; set; }
    public List<DomainCompany> TopCompanies { get; set; } = new();
    public List<DomainResearchReport> ResearchReports { get; set; } = new();
    public List<IndustryPerformanceEntry> TopPerformingCompanies { get; set; } = new();
    public List<IndustryGrowthEntry> TopGrowthCompanies { get; set; } = new();
}

public class IndustryPerformanceEntry
{
    public string Symbol { get; set; } = string.Empty;
    public string? Name { get; set; }
    public decimal? YtdReturn { get; set; }
    public decimal? LastPrice { get; set; }
    public decimal? TargetPrice { get; set; }
}

public class IndustryGrowthEntry
{
    public string Symbol { get; set; } = string.Empty;
    public string? Name { get; set; }
    public decimal? YtdReturn { get; set; }
    public decimal? GrowthEstimate { get; set; }
}

public class MarketData
{
    public string Market { get; set; } = string.Empty;
    public MarketStatus? Status { get; set; }
    public Dictionary<string, MarketSummaryEntry> Summary { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}

public class MarketStatus
{
    public string? MarketState { get; set; }
    public DateTime? Open { get; set; }
    public DateTime? Close { get; set; }
    public MarketTimezone? Timezone { get; set; }
}

public class MarketTimezone
{
    public string? ShortName { get; set; }
    public int? GmtOffset { get; set; }
}

public class MarketSummaryEntry
{
    public string? Exchange { get; set; }
    public string? ShortName { get; set; }
    public decimal? RegularMarketPrice { get; set; }
    public decimal? RegularMarketChange { get; set; }
    public decimal? RegularMarketChangePercent { get; set; }
}
