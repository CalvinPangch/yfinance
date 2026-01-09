using System.Linq;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Enums;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for fast info snapshot derived from history and metadata.
/// </summary>
public class FastInfoScraper : IFastInfoScraper
{
    private readonly IHistoryScraper _historyScraper;
    private readonly IQuoteScraper _quoteScraper;
    private readonly ISharesScraper _sharesScraper;

    /// <summary>
    /// Initializes a new instance of the <see cref="FastInfoScraper"/> class.
    /// </summary>
    /// <param name="historyScraper">Scraper for historical price data.</param>
    /// <param name="quoteScraper">Scraper for quote data.</param>
    /// <param name="sharesScraper">Scraper for shares outstanding data.</param>
    public FastInfoScraper(
        IHistoryScraper historyScraper,
        IQuoteScraper quoteScraper,
        ISharesScraper sharesScraper)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
        _quoteScraper = quoteScraper ?? throw new ArgumentNullException(nameof(quoteScraper));
        _sharesScraper = sharesScraper ?? throw new ArgumentNullException(nameof(sharesScraper));
    }

    /// <summary>
    /// Gets fast info data derived from history, quotes, and shares data.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Fast info data for the symbol.</returns>
    public async Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var historyRequest = new HistoryRequest
        {
            Period = Period.OneYear,
            Interval = Interval.OneDay,
            AutoAdjust = false,
            Repair = false
        };

        var historyTask = _historyScraper.GetHistoryAsync(symbol, historyRequest, cancellationToken);
        var metadataTask = _historyScraper.GetHistoryMetadataAsync(symbol, cancellationToken);
        var quoteTask = _quoteScraper.GetQuoteAsync(symbol, cancellationToken);
        var sharesTask = _sharesScraper.GetSharesHistoryAsync(new SharesHistoryRequest { Symbol = symbol }, cancellationToken);

        await Task.WhenAll(historyTask, metadataTask, quoteTask, sharesTask).ConfigureAwait(false);

        var history = historyTask.Result;
        var metadata = metadataTask.Result;
        var quote = quoteTask.Result;
        var shares = sharesTask.Result;

        var lastClose = history.Close.Length > 0 ? history.Close[^1] : (decimal?)null;
        var prevClose = history.Close.Length > 1 ? history.Close[^2] : (decimal?)null;

        var lastVolume = history.Volume.Length > 0 ? history.Volume[^1] : (long?)null;
        var lastPrice = quote.RegularMarketPrice ?? lastClose;

        var sharesOutstanding = shares.Entries
            .Where(entry => entry.SharesOutstanding.HasValue)
            .OrderBy(entry => entry.Date ?? DateTime.MinValue)
            .Select(entry => entry.SharesOutstanding)
            .LastOrDefault();

        var result = new FastInfoData
        {
            Symbol = symbol,
            Currency = metadata.Currency,
            QuoteType = metadata.InstrumentType,
            Exchange = metadata.ExchangeName,
            TimeZone = metadata.ExchangeTimezoneName,
            Shares = sharesOutstanding.HasValue ? (long?)Math.Round(sharesOutstanding.Value) : null,
            MarketCap = quote.MarketCap,
            LastPrice = lastPrice,
            PreviousClose = prevClose,
            Open = quote.RegularMarketOpen,
            DayHigh = quote.RegularMarketDayHigh,
            DayLow = quote.RegularMarketDayLow,
            RegularMarketPreviousClose = quote.RegularMarketPreviousClose,
            LastVolume = quote.RegularMarketVolume ?? lastVolume
        };

        PopulateHistoryMetrics(history, result);
        return result;
    }

    private static void PopulateHistoryMetrics(HistoricalData history, FastInfoData result)
    {
        if (history.Close.Length == 0)
            return;

        var closes = history.Close.Where(value => value > 0m).ToArray();
        var highs = history.High.Where(value => value > 0m).ToArray();
        var lows = history.Low.Where(value => value > 0m).ToArray();
        var volumes = history.Volume.Where(value => value > 0).Select(value => (decimal)value).ToArray();

        result.YearHigh = highs.Length > 0 ? highs.Max() : null;
        result.YearLow = lows.Length > 0 ? lows.Min() : null;

        if (closes.Length >= 2)
        {
            var first = closes.First();
            var last = closes.Last();
            if (first > 0m)
                result.YearChange = (last - first) / first;
        }

        result.FiftyDayAverage = AverageTail(closes, 50);
        result.TwoHundredDayAverage = AverageTail(closes, 200);
        result.TenDayAverageVolume = AverageTail(volumes, 10);
        result.ThreeMonthAverageVolume = AverageTail(volumes, 63);
    }

    private static decimal? AverageTail(decimal[] values, int count)
    {
        if (values.Length == 0)
            return null;

        var tail = values.Length > count ? values[^count..] : values;
        return tail.Length > 0 ? tail.Average() : null;
    }
}
