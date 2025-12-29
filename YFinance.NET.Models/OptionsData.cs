namespace YFinance.NET.Models;

/// <summary>
/// Option chain data for a ticker symbol.
/// </summary>
public class OptionChain
{
    public string Symbol { get; set; } = string.Empty;
    public DateTime? ExpirationDate { get; set; }
    public List<DateTime>? ExpirationDates { get; set; }
    public List<OptionContract> Calls { get; set; } = new();
    public List<OptionContract> Puts { get; set; } = new();
    public OptionUnderlying? Underlying { get; set; }
    public string? RawJson { get; set; }
}

/// <summary>
/// Single option contract entry.
/// </summary>
public class OptionContract
{
    public string? ContractSymbol { get; set; }
    public decimal? Strike { get; set; }
    public decimal? LastPrice { get; set; }
    public decimal? Bid { get; set; }
    public decimal? Ask { get; set; }
    public decimal? Change { get; set; }
    public decimal? PercentChange { get; set; }
    public long? Volume { get; set; }
    public long? OpenInterest { get; set; }
    public decimal? ImpliedVolatility { get; set; }
    public bool? InTheMoney { get; set; }
    public string? ContractSize { get; set; }
    public string? Currency { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? LastTradeDate { get; set; }
    public string? RawJson { get; set; }
}

/// <summary>
/// Underlying quote snapshot returned with the option chain.
/// </summary>
public class OptionUnderlying
{
    public string? Symbol { get; set; }
    public decimal? RegularMarketPrice { get; set; }
    public decimal? RegularMarketChange { get; set; }
    public decimal? RegularMarketChangePercent { get; set; }
    public string? Currency { get; set; }
    public string? Exchange { get; set; }
    public string? QuoteType { get; set; }
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public string? TimeZone { get; set; }
    public string? RawJson { get; set; }
}
