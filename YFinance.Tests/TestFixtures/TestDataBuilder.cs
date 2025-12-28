using System.Text.Json;

namespace YFinance.Tests.TestFixtures;

/// <summary>
/// Builder for creating test data that mimics Yahoo Finance API responses.
/// </summary>
public static class TestDataBuilder
{
    /// <summary>
    /// Builds a valid Yahoo Finance chart response JSON.
    /// </summary>
    /// <param name="symbol">The stock symbol.</param>
    /// <param name="bars">Array of price bars to include.</param>
    /// <returns>JSON string representing a valid chart response.</returns>
    public static string BuildValidChartResponse(string symbol, params PriceBar[] bars)
    {
        var timestamps = bars.Select(b => new DateTimeOffset(b.Date).ToUnixTimeSeconds()).ToArray();
        var opens = bars.Select(b => b.Open).ToArray();
        var highs = bars.Select(b => b.High).ToArray();
        var lows = bars.Select(b => b.Low).ToArray();
        var closes = bars.Select(b => b.Close).ToArray();
        var volumes = bars.Select(b => b.Volume).ToArray();

        var response = new
        {
            chart = new
            {
                result = new[]
                {
                    new
                    {
                        meta = new
                        {
                            symbol,
                            exchangeTimezoneName = "America/New_York"
                        },
                        timestamp = timestamps,
                        indicators = new
                        {
                            quote = new[]
                            {
                                new
                                {
                                    open = opens,
                                    high = highs,
                                    low = lows,
                                    close = closes,
                                    volume = volumes
                                }
                            },
                            adjclose = new[]
                            {
                                new
                                {
                                    adjclose = closes // For simplicity, use same as close
                                }
                            }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    /// <summary>
    /// Builds a chart response with dividends and splits.
    /// </summary>
    public static string BuildChartResponseWithEvents(string symbol, PriceBar[] bars,
        Dictionary<DateTime, decimal>? dividends = null,
        Dictionary<DateTime, (decimal numerator, decimal denominator)>? splits = null)
    {
        var timestamps = bars.Select(b => new DateTimeOffset(b.Date).ToUnixTimeSeconds()).ToArray();
        var opens = bars.Select(b => b.Open).ToArray();
        var highs = bars.Select(b => b.High).ToArray();
        var lows = bars.Select(b => b.Low).ToArray();
        var closes = bars.Select(b => b.Close).ToArray();
        var volumes = bars.Select(b => b.Volume).ToArray();

        var eventsObj = new Dictionary<string, object>();

        if (dividends != null && dividends.Count > 0)
        {
            var divsDict = new Dictionary<string, object>();
            foreach (var div in dividends)
            {
                var key = new DateTimeOffset(div.Key).ToUnixTimeSeconds().ToString();
                divsDict[key] = new
                {
                    date = new DateTimeOffset(div.Key).ToUnixTimeSeconds(),
                    amount = div.Value
                };
            }
            eventsObj["dividends"] = divsDict;
        }

        if (splits != null && splits.Count > 0)
        {
            var splitsDict = new Dictionary<string, object>();
            foreach (var split in splits)
            {
                var key = new DateTimeOffset(split.Key).ToUnixTimeSeconds().ToString();
                splitsDict[key] = new
                {
                    date = new DateTimeOffset(split.Key).ToUnixTimeSeconds(),
                    numerator = split.Value.numerator,
                    denominator = split.Value.denominator
                };
            }
            eventsObj["splits"] = splitsDict;
        }

        var resultObj = new
        {
            meta = new
            {
                symbol,
                exchangeTimezoneName = "America/New_York"
            },
            timestamp = timestamps,
            indicators = new
            {
                quote = new[]
                {
                    new
                    {
                        open = opens,
                        high = highs,
                        low = lows,
                        close = closes,
                        volume = volumes
                    }
                },
                adjclose = new[]
                {
                    new { adjclose = closes }
                }
            }
        };

        // Build response with events if any
        if (eventsObj.Count > 0)
        {
            var response = new
            {
                chart = new
                {
                    result = new[]
                    {
                        new Dictionary<string, object>
                        {
                            ["meta"] = resultObj.meta,
                            ["timestamp"] = resultObj.timestamp,
                            ["indicators"] = resultObj.indicators,
                            ["events"] = eventsObj
                        }
                    }
                }
            };
            return JsonSerializer.Serialize(response);
        }
        else
        {
            var response = new
            {
                chart = new
                {
                    result = new[] { resultObj }
                }
            };
            return JsonSerializer.Serialize(response);
        }
    }

    /// <summary>
    /// Builds a chart response with events but without adjusted close data.
    /// </summary>
    public static string BuildChartResponseWithEventsWithoutAdjClose(string symbol, PriceBar[] bars,
        Dictionary<DateTime, decimal>? dividends = null,
        Dictionary<DateTime, (decimal numerator, decimal denominator)>? splits = null)
    {
        var timestamps = bars.Select(b => new DateTimeOffset(b.Date).ToUnixTimeSeconds()).ToArray();
        var opens = bars.Select(b => b.Open).ToArray();
        var highs = bars.Select(b => b.High).ToArray();
        var lows = bars.Select(b => b.Low).ToArray();
        var closes = bars.Select(b => b.Close).ToArray();
        var volumes = bars.Select(b => b.Volume).ToArray();

        var eventsObj = new Dictionary<string, object>();

        if (dividends != null && dividends.Count > 0)
        {
            var divsDict = new Dictionary<string, object>();
            foreach (var div in dividends)
            {
                var key = new DateTimeOffset(div.Key).ToUnixTimeSeconds().ToString();
                divsDict[key] = new
                {
                    date = new DateTimeOffset(div.Key).ToUnixTimeSeconds(),
                    amount = div.Value
                };
            }
            eventsObj["dividends"] = divsDict;
        }

        if (splits != null && splits.Count > 0)
        {
            var splitsDict = new Dictionary<string, object>();
            foreach (var split in splits)
            {
                var key = new DateTimeOffset(split.Key).ToUnixTimeSeconds().ToString();
                splitsDict[key] = new
                {
                    date = new DateTimeOffset(split.Key).ToUnixTimeSeconds(),
                    numerator = split.Value.numerator,
                    denominator = split.Value.denominator
                };
            }
            eventsObj["splits"] = splitsDict;
        }

        var resultObj = new
        {
            meta = new
            {
                symbol,
                exchangeTimezoneName = "America/New_York"
            },
            timestamp = timestamps,
            indicators = new
            {
                quote = new[]
                {
                    new
                    {
                        open = opens,
                        high = highs,
                        low = lows,
                        close = closes,
                        volume = volumes
                    }
                }
            }
        };

        if (eventsObj.Count > 0)
        {
            var response = new
            {
                chart = new
                {
                    result = new[]
                    {
                        new Dictionary<string, object>
                        {
                            ["meta"] = resultObj.meta,
                            ["timestamp"] = resultObj.timestamp,
                            ["indicators"] = resultObj.indicators,
                            ["events"] = eventsObj
                        }
                    }
                }
            };
            return JsonSerializer.Serialize(response);
        }

        var emptyEventsResponse = new
        {
            chart = new
            {
                result = new[] { resultObj }
            }
        };

        return JsonSerializer.Serialize(emptyEventsResponse);
    }

    /// <summary>
    /// Builds an empty chart response (no data points).
    /// </summary>
    public static string BuildEmptyChartResponse(string symbol)
    {
        var response = new
        {
            chart = new
            {
                result = new[]
                {
                    new
                    {
                        meta = new
                        {
                            symbol,
                            exchangeTimezoneName = "UTC"
                        },
                        timestamp = Array.Empty<long>(),
                        indicators = new
                        {
                            quote = new[]
                            {
                                new
                                {
                                    open = Array.Empty<decimal>(),
                                    high = Array.Empty<decimal>(),
                                    low = Array.Empty<decimal>(),
                                    close = Array.Empty<decimal>(),
                                    volume = Array.Empty<long>()
                                }
                            }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    /// <summary>
    /// Builds a chart response without adjusted close data.
    /// </summary>
    public static string BuildChartResponseWithoutAdjClose(string symbol, params PriceBar[] bars)
    {
        var timestamps = bars.Select(b => new DateTimeOffset(b.Date).ToUnixTimeSeconds()).ToArray();
        var opens = bars.Select(b => b.Open).ToArray();
        var highs = bars.Select(b => b.High).ToArray();
        var lows = bars.Select(b => b.Low).ToArray();
        var closes = bars.Select(b => b.Close).ToArray();
        var volumes = bars.Select(b => b.Volume).ToArray();

        var response = new
        {
            chart = new
            {
                result = new[]
                {
                    new
                    {
                        meta = new
                        {
                            symbol,
                            exchangeTimezoneName = "America/New_York"
                        },
                        timestamp = timestamps,
                        indicators = new
                        {
                            quote = new[]
                            {
                                new
                                {
                                    open = opens,
                                    high = highs,
                                    low = lows,
                                    close = closes,
                                    volume = volumes
                                }
                            }
                            // Note: No adjclose array
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }
}

/// <summary>
/// Represents a single price bar (OHLCV data point).
/// </summary>
/// <param name="Date">The date of the bar.</param>
/// <param name="Open">Opening price.</param>
/// <param name="High">High price.</param>
/// <param name="Low">Low price.</param>
/// <param name="Close">Closing price.</param>
/// <param name="Volume">Trading volume.</param>
public record PriceBar(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, long Volume);
