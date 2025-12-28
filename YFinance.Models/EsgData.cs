namespace YFinance.Models;

/// <summary>
/// Environmental, Social, Governance (ESG) score data.
/// </summary>
public class EsgData
{
    public string Symbol { get; set; } = string.Empty;

    public decimal? TotalEsg { get; set; }
    public decimal? EnvironmentScore { get; set; }
    public decimal? SocialScore { get; set; }
    public decimal? GovernanceScore { get; set; }

    public int? RatingYear { get; set; }
    public int? RatingMonth { get; set; }
    public int? HighestControversy { get; set; }

    public int? PeerCount { get; set; }
    public string? PeerGroup { get; set; }

    public string? EsgPerformance { get; set; }
    public string? PeerEsgScorePerformance { get; set; }
    public string? PeerEnvironmentPerformance { get; set; }
    public string? PeerSocialPerformance { get; set; }
    public string? PeerGovernancePerformance { get; set; }

    public decimal? Percentile { get; set; }
    public decimal? EnvironmentPercentile { get; set; }
    public decimal? SocialPercentile { get; set; }
    public decimal? GovernancePercentile { get; set; }
}
