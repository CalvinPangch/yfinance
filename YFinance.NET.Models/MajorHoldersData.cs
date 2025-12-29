namespace YFinance.NET.Models;

/// <summary>
/// Major holders summary information.
/// Provides quick summary of insider and institutional ownership.
/// </summary>
public sealed class MajorHoldersData
{
    public string Symbol { get; init; } = string.Empty;

    /// <summary>
    /// Percentage of shares held by insiders
    /// </summary>
    public decimal? InsidersPercentHeld { get; init; }

    /// <summary>
    /// Percentage of shares held by institutions
    /// </summary>
    public decimal? InstitutionsPercentHeld { get; init; }

    /// <summary>
    /// Percentage of float held by institutions
    /// </summary>
    public decimal? InstitutionsFloatPercentHeld { get; init; }

    /// <summary>
    /// Number of institutional holders
    /// </summary>
    public int? InstitutionsCount { get; init; }

    /// <summary>
    /// Total shares outstanding
    /// </summary>
    public long? SharesOutstanding { get; init; }

    /// <summary>
    /// Floating shares
    /// </summary>
    public long? Float { get; init; }

    /// <summary>
    /// Percentage of float held by insiders and institutions combined
    /// </summary>
    public decimal? PercentInsiders { get; init; }

    /// <summary>
    /// Percentage of shares held by all institutions
    /// </summary>
    public decimal? PercentInstitutions { get; init; }
}
