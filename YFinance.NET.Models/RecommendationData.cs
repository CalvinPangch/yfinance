namespace YFinance.NET.Models;

/// <summary>
/// Recommendation trend entry for a period.
/// </summary>
public class RecommendationTrendEntry
{
    public string Period { get; set; } = string.Empty;
    public int? StrongBuy { get; set; }
    public int? Buy { get; set; }
    public int? Hold { get; set; }
    public int? Sell { get; set; }
    public int? StrongSell { get; set; }
}

/// <summary>
/// Upgrade/downgrade history entry.
/// </summary>
public class UpgradeDowngradeEntry
{
    public DateTime? GradeDate { get; set; }
    public string? Firm { get; set; }
    public string? ToGrade { get; set; }
    public string? FromGrade { get; set; }
    public string? Action { get; set; }
}
