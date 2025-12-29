namespace YFinance.NET.Models.Requests;

/// <summary>
/// Request model for Yahoo Finance news.
/// </summary>
public class NewsRequest
{
    public string Symbol { get; set; } = string.Empty;
    public int Count { get; set; } = 10;
    public string Tab { get; set; } = "news";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(Symbol));

        if (Count <= 0)
            throw new ArgumentException("Count must be greater than zero.", nameof(Count));
    }
}
