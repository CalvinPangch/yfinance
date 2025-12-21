using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Enums;
using YFinance.Models.Requests;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for retrieving historical price data from Yahoo Finance.
/// </summary>
public class HistoryScraper : IHistoryScraper
{
    private readonly IYahooFinanceClient _client;

    public HistoryScraper(IYahooFinanceClient client)
    {
        _client = client;
    }

    public async Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default)
    {
        request.Validate();

        var queryParams = BuildQueryParameters(request);
        var endpoint = $"/v8/finance/chart/{symbol}";

        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken);
        return ParseHistoricalData(symbol, jsonResponse);
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
        parameters["interval"] = ConvertIntervalToString(request.Interval);

        // Set events (include dividends and splits)
        parameters["events"] = "div,split";

        // Include premarket and postmarket
        parameters["includePrePost"] = "false";

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

    private HistoricalData ParseHistoricalData(string symbol, string jsonResponse)
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
        
        var open = ParseDecimalArray(quote, "open");
        var high = ParseDecimalArray(quote, "high");
        var low = ParseDecimalArray(quote, "low");
        var close = ParseDecimalArray(quote, "close");
        var volume = ParseLongArray(quote, "volume");

        // Get adjusted close
        decimal[] adjClose;
        try
        {
            var adjCloseData = result.GetProperty("indicators").GetProperty("adjclose")[0];
            adjClose = ParseDecimalArray(adjCloseData, "adjclose");
        }
        catch
        {
            // If no adjusted close, use regular close
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

        return new HistoricalData
        {
            Symbol = symbol,
            Timestamps = timestamps.ToArray(),
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

    private decimal[] ParseDecimalArray(JsonElement element, string propertyName)
    {
        var property = element.GetProperty(propertyName);
        var result = new List<decimal>();

        foreach (var item in property.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.Null)
            {
                result.Add(0);
            }
            else
            {
                result.Add(item.GetDecimal());
            }
        }

        return result.ToArray();
    }

    private long[] ParseLongArray(JsonElement element, string propertyName)
    {
        var property = element.GetProperty(propertyName);
        var result = new List<long>();

        foreach (var item in property.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.Null)
            {
                result.Add(0);
            }
            else
            {
                result.Add(item.GetInt64());
            }
        }

        return result.ToArray();
    }
}
