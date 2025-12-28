namespace YFinance.Models;

/// <summary>
/// ETF and mutual fund data.
/// </summary>
public class FundsData
{
    public string Symbol { get; set; } = string.Empty;
    public string? QuoteType { get; set; }
    public string? Description { get; set; }
    public FundOverview? Overview { get; set; }
    public List<FundOperationEntry> FundOperations { get; set; } = new();
    public Dictionary<string, decimal?> AssetClasses { get; set; } = new();
    public List<TopHolding> TopHoldings { get; set; } = new();
    public List<FundMetricEntry> EquityHoldings { get; set; } = new();
    public List<FundMetricEntry> BondHoldings { get; set; } = new();
    public Dictionary<string, decimal?> BondRatings { get; set; } = new();
    public Dictionary<string, decimal?> SectorWeightings { get; set; } = new();
}

public class FundOverview
{
    public string? CategoryName { get; set; }
    public string? Family { get; set; }
    public string? LegalType { get; set; }
}

public class FundOperationEntry
{
    public string Attribute { get; set; } = string.Empty;
    public decimal? FundValue { get; set; }
    public decimal? CategoryAverage { get; set; }
}

public class FundMetricEntry
{
    public string Metric { get; set; } = string.Empty;
    public decimal? FundValue { get; set; }
    public decimal? CategoryAverage { get; set; }
}

public class TopHolding
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public decimal? HoldingPercent { get; set; }
}
