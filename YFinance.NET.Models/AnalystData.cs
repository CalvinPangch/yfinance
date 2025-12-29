namespace YFinance.NET.Models;

/// <summary>
/// Analyst recommendations, price targets, and upgrades/downgrades.
/// </summary>
public class AnalystData
{
    public string Symbol { get; set; } = string.Empty;

    // Current recommendations
    public int? StrongBuy { get; set; }
    public int? Buy { get; set; }
    public int? Hold { get; set; }
    public int? Sell { get; set; }
    public int? StrongSell { get; set; }

    // Price targets
    public decimal? TargetHighPrice { get; set; }
    public decimal? TargetLowPrice { get; set; }
    public decimal? TargetMeanPrice { get; set; }
    public decimal? TargetMedianPrice { get; set; }

    // Analyst count
    public int? NumberOfAnalystOpinions { get; set; }

    // Recommendation history
    public List<RecommendationHistory>? RecommendationHistory { get; set; }

    // Upgrades/downgrades
    public List<UpgradeDowngrade>? UpgradesDowngrades { get; set; }
}

public class RecommendationHistory
{
    public DateTime Date { get; set; }
    public string Firm { get; set; } = string.Empty;
    public string ToGrade { get; set; } = string.Empty;
    public string? FromGrade { get; set; }
    public string Action { get; set; } = string.Empty;
}

public class UpgradeDowngrade
{
    public DateTime Date { get; set; }
    public string Firm { get; set; } = string.Empty;
    public string ToGrade { get; set; } = string.Empty;
    public string? FromGrade { get; set; }
    public string Action { get; set; } = string.Empty;
}
