namespace YFinance.Models.Requests;

/// <summary>
/// Request model for Yahoo Finance option chain.
/// </summary>
public class OptionChainRequest
{
    public string Symbol { get; set; } = string.Empty;
    public DateTime? ExpirationDate { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(Symbol));
    }
}
