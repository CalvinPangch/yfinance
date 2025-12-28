using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Implementation.Constants;
using YFinance.Models;
using YFinance.Models.Enums;
using YFinance.Models.Exceptions;
using YFinance.Models.Requests;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for retrieving historical price data from Yahoo Finance.
/// </summary>
public class HistoryScraper : IHistoryScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;
    private readonly IPriceRepair _priceRepair;
    private readonly ITimezoneHelper _timezoneHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="HistoryScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
    public HistoryScraper(
        IYahooFinanceClient client,
        IDataParser dataParser,
        IPriceRepair priceRepair,
        ITimezoneHelper timezoneHelper)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        _priceRepair = priceRepair ?? throw new ArgumentNullException(nameof(priceRepair));
        _timezoneHelper = timezoneHelper ?? throw new ArgumentNullException(nameof(timezoneHelper));
    }

    /// <inheritdoc />
    public async Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        var queryParams = BuildQueryParameters(request);
        var endpoint = $"/v8/finance/chart/{symbol}";

        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseHistoricalData(symbol, jsonResponse, request);
    }

    private Dictionary<string, string> BuildQueryParameters(HistoryRequest request)
    {
        var parameters = new Dictionary<string, string>();

        // Set period or date range
        if (request.Period.HasValue)
        {
            parameters["range"] = ConvertPeriodToRange(request.Period.Value);
        }
        else if (request.Start.HasValue && request.End.HasValue)
        {
            parameters["period1"] = ((DateTimeOffset)request.Start.Value).ToUnixTimeSeconds().ToString();
            parameters["period2"] = ((DateTimeOffset)request.End.Value).ToUnixTimeSeconds().ToString();
        }

        // Set interval
        parameters[YahooFinanceConstants.QueryParams.Interval] = ConvertIntervalToString(request.Interval);

        // Set events (include dividends and splits)
        parameters["events"] = YahooFinanceConstants.QueryParams.Events;

        // Include premarket and postmarket
        parameters["includePrePost"] = YahooFinanceConstants.QueryParams.IncludePrePost;

        return parameters;
    }

    private string ConvertPeriodToRange(Period period)
    {
        return period switch
        {
            Period.OneDay => "1d",
            Period.FiveDays => "5d",
            Period.OneMonth => "1mo",
            Period.ThreeMonths => "3mo",
            Period.SixMonths => "6mo",
            Period.OneYear => "1y",
            Period.TwoYears => "2y",
            Period.FiveYears => "5y",
            Period.TenYears => "10y",
            Period.YearToDate => "ytd",
            Period.Max => "max",
            _ => "1mo"
        };
    }

    private string ConvertIntervalToString(Interval interval)
    {
        return interval switch
        {
            Interval.OneMinute => "1m",
            Interval.TwoMinutes => "2m",
            Interval.FiveMinutes => "5m",
            Interval.FifteenMinutes => "15m",
            Interval.ThirtyMinutes => "30m",
            Interval.SixtyMinutes => "60m",
            Interval.NinetyMinutes => "90m",
            Interval.OneHour => "1h",
            Interval.OneDay => "1d",
            Interval.FiveDays => "5d",
            Interval.OneWeek => "1wk",
            Interval.OneMonth => "1mo",
            Interval.ThreeMonths => "3mo",
            _ => "1d"
        };
    }

    private HistoricalData ParseHistoricalData(string symbol, string jsonResponse, HistoryRequest request)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        // Navigate to chart.result[0]
        var result = root.GetProperty("chart").GetProperty("result")[0];

        // Get timezone
        var meta = result.GetProperty("meta");
        var timezone = meta.GetProperty("exchangeTimezoneName").GetString() ?? "UTC";

        // Get timestamps
        var timestampsArray = result.GetProperty("timestamp");
        var timestamps = new List<DateTime>();
        foreach (var ts in timestampsArray.EnumerateArray())
        {
            timestamps.Add(DateTimeOffset.FromUnixTimeSeconds(ts.GetInt64()).UtcDateTime);
        }

        // Get quote data (OHLC)
        var quote = result.GetProperty("indicators").GetProperty("quote")[0];

        var open = _dataParser.ParseDecimalArray(quote, "open");
        var high = _dataParser.ParseDecimalArray(quote, "high");
        var low = _dataParser.ParseDecimalArray(quote, "low");
        var close = _dataParser.ParseDecimalArray(quote, "close");
        var volume = _dataParser.ParseLongArray(quote, "volume");

        // Get adjusted close - use regular close as fallback if unavailable
        decimal[] adjClose;
        try
        {
            var adjCloseData = result.GetProperty("indicators").GetProperty("adjclose")[0];
            adjClose = _dataParser.ParseDecimalArray(adjCloseData, "adjclose");
        }
        catch (JsonException)
        {
            // If no adjusted close data available, use regular close as fallback
            adjClose = close;
        }
        catch (KeyNotFoundException)
        {
            // Missing adjclose property, use regular close
            adjClose = close;
        }

        // Get dividends and splits
        var dividends = new Dictionary<DateTime, decimal>();
        var splits = new Dictionary<DateTime, decimal>();

        if (result.TryGetProperty("events", out var events))
        {
            if (events.TryGetProperty("dividends", out var divsElement))
            {
                foreach (var div in divsElement.EnumerateObject())
                {
                    var divData = div.Value;
                    var date = DateTimeOffset.FromUnixTimeSeconds(divData.GetProperty("date").GetInt64()).UtcDateTime;
                    var amount = divData.GetProperty("amount").GetDecimal();
                    dividends[date] = amount;
                }
            }

            if (events.TryGetProperty("splits", out var splitsElement))
            {
                foreach (var split in splitsElement.EnumerateObject())
                {
                    var splitData = split.Value;
                    var date = DateTimeOffset.FromUnixTimeSeconds(splitData.GetProperty("date").GetInt64()).UtcDateTime;
                    var numerator = splitData.GetProperty("numerator").GetDecimal();
                    var denominator = splitData.GetProperty("denominator").GetDecimal();
                    splits[date] = numerator / denominator;
                }
            }
        }

        var timestampArray = _timezoneHelper.FixDstIssues(timestamps.ToArray(), timezone);

        // Apply price repair if requested
        if (request.Repair)
        {
            open = _priceRepair.RepairPrices(open, timestampArray, splits);
            high = _priceRepair.RepairPrices(high, timestampArray, splits);
            low = _priceRepair.RepairPrices(low, timestampArray, splits);
            close = _priceRepair.RepairPrices(close, timestampArray, splits);
            adjClose = _priceRepair.RepairPrices(adjClose, timestampArray, splits);
        }

        // Auto-adjust prices using adjusted close ratio
        if (request.AutoAdjust)
        {
            ApplyAutoAdjust(open, high, low, close, adjClose);
        }

        return new HistoricalData
        {
            Symbol = symbol,
            Timestamps = timestampArray,
            Open = open,
            High = high,
            Low = low,
            Close = close,
            AdjustedClose = adjClose,
            Volume = volume,
            Dividends = dividends,
            StockSplits = splits,
            TimeZone = timezone
        };
    }

    private static void ApplyAutoAdjust(decimal[] open, decimal[] high, decimal[] low, decimal[] close, decimal[] adjClose)
    {
        var length = Math.Min(close.Length, adjClose.Length);
        for (int i = 0; i < length; i++)
        {
            if (close[i] == 0m)
                continue;

            var factor = adjClose[i] / close[i];
            if (factor <= 0m)
                continue;

            open[i] *= factor;
            high[i] *= factor;
            low[i] *= factor;
            close[i] = adjClose[i];
        }
    }

}
