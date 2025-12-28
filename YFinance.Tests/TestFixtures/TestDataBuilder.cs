using System.Collections.Generic;
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

    public static string BuildSearchResponse(string symbol)
    {
        var response = new
        {
            quotes = new[]
            {
                new
                {
                    symbol,
                    shortname = "Test Corp",
                    longname = "Test Corporation",
                    exchange = "NMS",
                    quoteType = "EQUITY",
                    score = 0.75m
                }
            },
            news = new[]
            {
                new
                {
                    title = "Test News",
                    publisher = "NewsWire",
                    link = "https://example.com/news",
                    providerPublishTime = 1700000000,
                    type = "STORY",
                    uuid = "news-1"
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    public static string BuildLookupResponse(string symbol)
    {
        var response = new
        {
            finance = new
            {
                result = new[]
                {
                    new
                    {
                        documents = new[]
                        {
                            new
                            {
                                symbol,
                                shortName = "Test Corp",
                                longName = "Test Corporation",
                                exchange = "NMS",
                                quoteType = "EQUITY",
                                regularMarketPrice = 150.25m,
                                regularMarketChangePercent = 0.5m
                            }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    public static string BuildScreenerResponse(string symbol)
    {
        var response = new
        {
            finance = new
            {
                result = new[]
                {
                    new
                    {
                        count = 1,
                        total = 1,
                        quotes = new[]
                        {
                            new
                            {
                                symbol,
                                shortName = "Test Corp",
                                longName = "Test Corporation",
                                exchange = "NMS",
                                quoteType = "EQUITY",
                                regularMarketPrice = 150.25m,
                                regularMarketChangePercent = 0.5m,
                                regularMarketVolume = 1000000
                            }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    public static string BuildFundsResponse(string symbol)
    {
        var response = new
        {
            quoteSummary = new
            {
                result = new[]
                {
                    new
                    {
                        quoteType = new
                        {
                            quoteType = "ETF"
                        },
                        summaryProfile = new
                        {
                            longBusinessSummary = "Fund description"
                        },
                        topHoldings = new
                        {
                            cashPosition = new { raw = 0.05m },
                            stockPosition = new { raw = 0.9m },
                            bondPosition = new { raw = 0.02m },
                            preferredPosition = new { raw = 0.0m },
                            convertiblePosition = new { raw = 0.0m },
                            otherPosition = new { raw = 0.03m },
                            holdings = new[]
                            {
                                new { symbol = "AAPL", holdingName = "Apple", holdingPercent = new { raw = 0.05m } }
                            },
                            equityHoldings = new
                            {
                                priceToEarnings = new { raw = 20m },
                                priceToEarningsCat = new { raw = 22m },
                                priceToBook = new { raw = 3m },
                                priceToBookCat = new { raw = 3.2m },
                                priceToSales = new { raw = 4m },
                                priceToSalesCat = new { raw = 4.1m },
                                priceToCashflow = new { raw = 10m },
                                priceToCashflowCat = new { raw = 11m },
                                medianMarketCap = new { raw = 50000000000m },
                                medianMarketCapCat = new { raw = 52000000000m },
                                threeYearEarningsGrowth = new { raw = 0.1m },
                                threeYearEarningsGrowthCat = new { raw = 0.12m }
                            },
                            bondHoldings = new
                            {
                                duration = new { raw = 5m },
                                durationCat = new { raw = 6m },
                                maturity = new { raw = 7m },
                                maturityCat = new { raw = 8m },
                                creditQuality = new { raw = 9m },
                                creditQualityCat = new { raw = 10m }
                            },
                            bondRatings = new object[]
                            {
                                new Dictionary<string, decimal> { ["AAA"] = 0.2m },
                                new Dictionary<string, decimal> { ["AA"] = 0.3m }
                            },
                            sectorWeightings = new object[]
                            {
                                new Dictionary<string, decimal> { ["Technology"] = 0.4m },
                                new Dictionary<string, decimal> { ["Financial"] = 0.1m }
                            }
                        },
                        fundProfile = new
                        {
                            categoryName = "Large Blend",
                            family = "Vanguard",
                            legalType = "ETF",
                            feesExpensesInvestment = new
                            {
                                annualReportExpenseRatio = new { raw = 0.04m },
                                annualHoldingsTurnover = new { raw = 0.1m },
                                totalNetAssets = new { raw = 12345m }
                            },
                            feesExpensesInvestmentCat = new
                            {
                                annualReportExpenseRatio = new { raw = 0.05m },
                                annualHoldingsTurnover = new { raw = 0.2m },
                                totalNetAssets = new { raw = 23456m }
                            }
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
