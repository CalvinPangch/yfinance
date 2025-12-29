namespace YFinance.Models;

/// <summary>
/// Fast access to common ticker information.
/// Provides lightweight, quick access to key metrics without full quote data.
/// </summary>
public sealed class FastInfo
{
    public string Symbol { get; init; } = string.Empty;

    // Price data
    public decimal? LastPrice { get; init; }
    public decimal? PreviousClose { get; init; }
    public decimal? Open { get; init; }
    public decimal? DayHigh { get; init; }
    public decimal? DayLow { get; init; }

    // 52-week range
    public decimal? YearHigh { get; init; }
    public decimal? YearLow { get; init; }

    // Volume
    public long? Volume { get; init; }
    public long? AverageVolume { get; init; }
    public long? AverageVolume10Day { get; init; }

    // Market metrics
    public decimal? MarketCap { get; init; }
    public long? SharesOutstanding { get; init; }
    public decimal? Float { get; init; }

    // Basic info
    public string Currency { get; init; } = string.Empty;
    public string Exchange { get; init; } = string.Empty;
    public string QuoteType { get; init; } = string.Empty;
    public string TimeZone { get; init; } = string.Empty;

    // Performance metrics
    public decimal? PeRatio { get; init; }
    public decimal? ForwardPE { get; init; }
    public decimal? DividendYield { get; init; }
    public decimal? Beta { get; init; }

    // Timestamp
    public DateTime? LastUpdated { get; init; }
}
