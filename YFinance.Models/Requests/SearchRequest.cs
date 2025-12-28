namespace YFinance.Models.Requests;

/// <summary>
/// Request model for Yahoo Finance search.
/// </summary>
public class SearchRequest
{
    public string Query { get; set; } = string.Empty;
    public int QuotesCount { get; set; } = 8;
    public int NewsCount { get; set; } = 8;
    public int ListsCount { get; set; } = 8;
    public bool IncludeCompanyBreakdown { get; set; } = true;
    public bool IncludeNavLinks { get; set; } = false;
    public bool IncludeResearchReports { get; set; } = false;
    public bool IncludeCulturalAssets { get; set; } = false;
    public bool EnableFuzzyQuery { get; set; } = false;
    public int RecommendedCount { get; set; } = 8;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Query))
            throw new ArgumentException("Query cannot be null or empty.", nameof(Query));

        if (QuotesCount < 0 || NewsCount < 0 || ListsCount < 0 || RecommendedCount < 0)
            throw new ArgumentException("Counts cannot be negative.");
    }
}
