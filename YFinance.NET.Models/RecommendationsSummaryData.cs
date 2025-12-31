namespace YFinance.NET.Models;

/// <summary>
/// Summary of analyst recommendations aggregated by time period.
/// Mirrors Python yfinance's ticker.recommendations_summary DataFrame.
/// </summary>
public class RecommendationsSummaryData
{
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// List of recommendation summaries by period (e.g., "-1m", "-2m", "-3m", "0m").
    /// </summary>
    public List<RecommendationsSummaryEntry> Summaries { get; set; } = new();
}

/// <summary>
/// Represents a single period's recommendation summary.
/// </summary>
public class RecommendationsSummaryEntry
{
    /// <summary>
    /// Time period relative to present (e.g., "0m" for current month, "-1m" for 1 month ago).
    /// </summary>
    public string Period { get; set; } = string.Empty;

    /// <summary>
    /// Number of strong buy recommendations in this period.
    /// </summary>
    public int? StrongBuy { get; set; }

    /// <summary>
    /// Number of buy recommendations in this period.
    /// </summary>
    public int? Buy { get; set; }

    /// <summary>
    /// Number of hold recommendations in this period.
    /// </summary>
    public int? Hold { get; set; }

    /// <summary>
    /// Number of sell recommendations in this period.
    /// </summary>
    public int? Sell { get; set; }

    /// <summary>
    /// Number of strong sell recommendations in this period.
    /// </summary>
    public int? StrongSell { get; set; }
}
