namespace YFinance.Models;

/// <summary>
/// Calendar event data for a ticker symbol.
/// </summary>
public class CalendarData
{
    public string Symbol { get; set; } = string.Empty;

    // Earnings
    public List<DateTime>? EarningsDates { get; set; }
    public decimal? EarningsAverage { get; set; }
    public decimal? EarningsLow { get; set; }
    public decimal? EarningsHigh { get; set; }
    public decimal? RevenueAverage { get; set; }
    public decimal? RevenueLow { get; set; }
    public decimal? RevenueHigh { get; set; }

    // Dividends
    public DateTime? DividendDate { get; set; }
    public DateTime? ExDividendDate { get; set; }
    public decimal? DividendAmount { get; set; }

    // Capital gains (funds)
    public DateTime? CapitalGainsDate { get; set; }
    public decimal? CapitalGainsAmount { get; set; }
}
