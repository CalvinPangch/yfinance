namespace YFinance.NET.Models.Requests;

/// <summary>
/// Request model for shares outstanding history.
/// </summary>
public class SharesHistoryRequest
{
    public string Symbol { get; set; } = string.Empty;
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(Symbol));
    }
}
