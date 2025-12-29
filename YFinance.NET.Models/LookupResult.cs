namespace YFinance.NET.Models;

/// <summary>
/// Yahoo Finance lookup results.
/// </summary>
public class LookupResult
{
    public string Query { get; set; } = string.Empty;
    public List<LookupDocument> Documents { get; set; } = new();
    public string RawResponse { get; set; } = string.Empty;
}

public class LookupDocument
{
    public string? Symbol { get; set; }
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public string? Exchange { get; set; }
    public string? QuoteType { get; set; }
    public string? Region { get; set; }
    public string? Currency { get; set; }
    public decimal? RegularMarketPrice { get; set; }
    public decimal? RegularMarketChangePercent { get; set; }
    public decimal? MarketCap { get; set; }
    public string RawJson { get; set; } = string.Empty;
}
