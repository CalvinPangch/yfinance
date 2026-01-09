namespace YFinance.NET.Models;

/// <summary>
/// Shares outstanding and float history.
/// </summary>
public class SharesHistoryData
{
    /// <summary>
    /// Gets or sets the ticker symbol.
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of shares history entries.
    /// </summary>
    public List<SharesHistoryEntry> Entries { get; set; } = new();
}

/// <summary>
/// Represents a single shares history entry.
/// </summary>
public class SharesHistoryEntry
{
    /// <summary>
    /// Gets or sets the date of the entry.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the number of shares outstanding.
    /// </summary>
    public decimal? SharesOutstanding { get; set; }

    /// <summary>
    /// Gets or sets the number of float shares.
    /// </summary>
    public decimal? FloatShares { get; set; }
}
