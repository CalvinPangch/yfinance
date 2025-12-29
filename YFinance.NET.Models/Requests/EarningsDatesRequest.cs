namespace YFinance.NET.Models.Requests;

/// <summary>
/// Request model for Yahoo Finance earnings dates.
/// </summary>
public class EarningsDatesRequest
{
    public string Symbol { get; set; } = string.Empty;
    public int Limit { get; set; } = 12;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(Symbol));

        if (Limit <= 0)
            throw new ArgumentException("Limit must be greater than zero.", nameof(Limit));
    }
}
