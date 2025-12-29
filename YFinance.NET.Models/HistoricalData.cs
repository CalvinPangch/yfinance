namespace YFinance.NET.Models;

/// <summary>
/// Historical OHLC price data with dividends and stock splits.
/// </summary>
public class HistoricalData
{
    public string Symbol { get; set; } = string.Empty;
    public DateTime[] Timestamps { get; set; } = Array.Empty<DateTime>();
    public decimal[] Open { get; set; } = Array.Empty<decimal>();
    public decimal[] High { get; set; } = Array.Empty<decimal>();
    public decimal[] Low { get; set; } = Array.Empty<decimal>();
    public decimal[] Close { get; set; } = Array.Empty<decimal>();
    public decimal[] AdjustedClose { get; set; } = Array.Empty<decimal>();
    public long[] Volume { get; set; } = Array.Empty<long>();

    // Corporate actions
    public Dictionary<DateTime, decimal> Dividends { get; set; } = new();
    public Dictionary<DateTime, decimal> StockSplits { get; set; } = new();
    public Dictionary<DateTime, decimal> CapitalGains { get; set; } = new();

    public string? TimeZone { get; set; }
}
