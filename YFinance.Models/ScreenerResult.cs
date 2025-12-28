namespace YFinance.Models;

/// <summary>
/// Yahoo Finance screener results.
/// </summary>
public class ScreenerResult
{
    public int? Count { get; set; }
    public int? Total { get; set; }
    public List<ScreenerQuote> Quotes { get; set; } = new();
    public string RawResponse { get; set; } = string.Empty;
}

public class ScreenerQuote
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
    public long? RegularMarketVolume { get; set; }
    public string RawJson { get; set; } = string.Empty;
}
