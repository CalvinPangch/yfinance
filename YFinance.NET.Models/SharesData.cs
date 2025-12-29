namespace YFinance.NET.Models;

/// <summary>
/// Shares outstanding and float history.
/// </summary>
public class SharesHistoryData
{
    public string Symbol { get; set; } = string.Empty;
    public List<SharesHistoryEntry> Entries { get; set; } = new();
}

public class SharesHistoryEntry
{
    public DateTime? Date { get; set; }
    public decimal? SharesOutstanding { get; set; }
    public decimal? FloatShares { get; set; }
}
