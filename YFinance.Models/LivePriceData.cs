namespace YFinance.Models;

/// <summary>
/// Live pricing payload from Yahoo Finance streamer.
/// </summary>
public class LivePriceData
{
    public string? Id { get; set; }
    public float? Price { get; set; }
    public long? Time { get; set; }
    public string? Currency { get; set; }
    public string? Exchange { get; set; }
    public int? QuoteType { get; set; }
    public int? MarketHours { get; set; }
    public float? ChangePercent { get; set; }
    public long? DayVolume { get; set; }
    public float? DayHigh { get; set; }
    public float? DayLow { get; set; }
    public float? Change { get; set; }
    public string? ShortName { get; set; }
    public long? ExpireDate { get; set; }
    public float? OpenPrice { get; set; }
    public float? PreviousClose { get; set; }
    public float? StrikePrice { get; set; }
    public string? UnderlyingSymbol { get; set; }
    public long? OpenInterest { get; set; }
    public long? OptionsType { get; set; }
    public long? MiniOption { get; set; }
    public long? LastSize { get; set; }
    public float? Bid { get; set; }
    public long? BidSize { get; set; }
    public float? Ask { get; set; }
    public long? AskSize { get; set; }
    public long? PriceHint { get; set; }
    public long? Volume24Hr { get; set; }
    public long? VolumeAllCurrencies { get; set; }
    public string? FromCurrency { get; set; }
    public string? LastMarket { get; set; }
    public double? CirculatingSupply { get; set; }
    public double? MarketCap { get; set; }
}
