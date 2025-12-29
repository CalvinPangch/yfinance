namespace YFinance.NET.Models;

/// <summary>
/// Metadata returned by Yahoo Finance chart endpoint.
/// </summary>
public class HistoryMetadata
{
    public string Symbol { get; set; } = string.Empty;
    public string? Currency { get; set; }
    public string? ExchangeName { get; set; }
    public string? ExchangeTimezoneName { get; set; }
    public string? InstrumentType { get; set; }
    public long? FirstTradeDate { get; set; }
    public long? RegularMarketTime { get; set; }
    public int? GmtOffset { get; set; }
    public TradingPeriods? CurrentTradingPeriod { get; set; }
}

public class TradingPeriods
{
    public TradingPeriod? Pre { get; set; }
    public TradingPeriod? Regular { get; set; }
    public TradingPeriod? Post { get; set; }
}

public class TradingPeriod
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
