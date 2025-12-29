namespace YFinance.NET.Models;

/// <summary>
/// Lightweight quote snapshot derived from history and metadata.
/// </summary>
public class FastInfoData
{
    public string Symbol { get; set; } = string.Empty;
    public string? Currency { get; set; }
    public string? QuoteType { get; set; }
    public string? Exchange { get; set; }
    public string? TimeZone { get; set; }

    public long? Shares { get; set; }
    public decimal? MarketCap { get; set; }

    public decimal? LastPrice { get; set; }
    public decimal? PreviousClose { get; set; }
    public decimal? Open { get; set; }
    public decimal? DayHigh { get; set; }
    public decimal? DayLow { get; set; }
    public decimal? RegularMarketPreviousClose { get; set; }
    public long? LastVolume { get; set; }

    public decimal? FiftyDayAverage { get; set; }
    public decimal? TwoHundredDayAverage { get; set; }
    public decimal? TenDayAverageVolume { get; set; }
    public decimal? ThreeMonthAverageVolume { get; set; }

    public decimal? YearHigh { get; set; }
    public decimal? YearLow { get; set; }
    public decimal? YearChange { get; set; }
}
