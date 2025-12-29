namespace YFinance.Models;

/// <summary>
/// Corporate actions for a ticker over a time range.
/// </summary>
public class ActionsData
{
    public string Symbol { get; set; } = string.Empty;
    public Dictionary<DateTime, decimal> Dividends { get; set; } = new();
    public Dictionary<DateTime, decimal> StockSplits { get; set; } = new();
    public Dictionary<DateTime, decimal> CapitalGains { get; set; } = new();
}
