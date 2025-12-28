namespace YFinance.Models;

/// <summary>
/// Quote data for a ticker symbol.
/// Contains current price, market cap, ratios, and other quote information.
/// </summary>
public class QuoteData
{
    public string Symbol { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string LongName { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string QuoteType { get; set; } = string.Empty;

    // Price data
    public decimal? RegularMarketPrice { get; set; }
    public decimal? RegularMarketOpen { get; set; }
    public decimal? RegularMarketDayHigh { get; set; }
    public decimal? RegularMarketDayLow { get; set; }
    public decimal? RegularMarketPreviousClose { get; set; }
    public long? RegularMarketVolume { get; set; }

    // Market metrics
    public decimal? MarketCap { get; set; }
    public decimal? PeRatio { get; set; }
    public decimal? ForwardPE { get; set; }
    public decimal? PegRatio { get; set; }
    public decimal? PriceToBook { get; set; }
    public decimal? DividendYield { get; set; }
    public decimal? EarningsPerShare { get; set; }
    public decimal? Beta { get; set; }

    // 52-week data
    public decimal? FiftyTwoWeekHigh { get; set; }
    public decimal? FiftyTwoWeekLow { get; set; }

    // Average volumes
    public long? AverageDailyVolume10Day { get; set; }
    public long? AverageDailyVolume3Month { get; set; }

    // Earnings and calendar data
    public DateTime? NextEarningsDate { get; set; }
    public decimal? EarningsAverage { get; set; }
    public decimal? EarningsLow { get; set; }
    public decimal? EarningsHigh { get; set; }
    public decimal? RevenueAverage { get; set; }
    public decimal? RevenueLow { get; set; }
    public decimal? RevenueHigh { get; set; }
    public DateTime? ExDividendDate { get; set; }

    // Timeseries metrics
    public decimal? TrailingPegRatio { get; set; }

    // Additional info
    public DateTime? EarningsTimestamp { get; set; }
    public string? TimeZone { get; set; }

    // SEC filings
    public List<SecFiling>? SecFilings { get; set; }
}

public class SecFiling
{
    public DateTime? FilingDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FormType { get; set; } = string.Empty;
    public string? EdgarUrl { get; set; }
}
