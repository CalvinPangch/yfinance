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

    public static string BuildNewsResponse(string symbol)
    {
        var response = new
        {
            data = new
            {
                tickerStream = new
                {
                    stream = new object[]
                    {
                        new
                        {
                            title = "News Title",
                            publisher = "Publisher",
                            link = "https://example.com/article",
                            providerPublishTime = 1700000000,
                            type = "STORY",
                            uuid = "news-1",
                            relatedTickers = new[] { symbol }
                        },
                        new
                        {
                            ad = new object[] { "sponsored" }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    public static string BuildEarningsSummaryResponse()
    {
        var response = new
        {
            quoteSummary = new
            {
                result = new object[]
                {
                    new
                    {
                        earningsTrend = new
                        {
                            trend = new object[]
                            {
                                new
                                {
                                    period = "0q",
                                    growth = new { raw = 0.1m },
                                    earningsEstimate = new
                                    {
                                        avg = new { raw = 1.2m },
                                        low = new { raw = 1.0m },
                                        high = new { raw = 1.4m }
                                    },
                                    revenueEstimate = new
                                    {
                                        avg = new { raw = 1000m },
                                        low = new { raw = 900m },
                                        high = new { raw = 1100m }
                                    },
                                    epsTrend = new
                                    {
                                        current = new { raw = 1.1m },
                                        sevenDaysAgo = new { raw = 1.0m }
                                    },
                                    epsRevisions = new
                                    {
                                        upLast7days = new { raw = 2m },
                                        downLast30days = new { raw = 1m }
                                    }
                                }
                            }
                        },
                        earningsHistory = new
                        {
                            history = new object[]
                            {
                                new
                                {
                                    quarter = new { raw = 1704067200 },
                                    epsActual = new { raw = 1.05m },
                                    epsEstimate = new { raw = 1.0m },
                                    epsDifference = new { raw = 0.05m },
                                    surprisePercent = new { raw = 0.05m }
                                }
                            }
                        },
                        industryTrend = new
                        {
                            trend = new object[]
                            {
                                new { period = "0q", growth = new { raw = 0.2m } }
                            }
                        },
                        sectorTrend = new
                        {
                            trend = new object[]
                            {
                                new { period = "0q", growth = new { raw = 0.3m } }
                            }
                        },
                        indexTrend = new
                        {
                            trend = new object[]
                            {
                                new { period = "0q", growth = new { raw = 0.4m } }
                            }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    public static string BuildEarningsDatesResponse()
    {
        var response = new
        {
            finance = new
            {
                result = new object[]
                {
                    new
                    {
                        columns = new object[]
                        {
                            new { field = "startdatetime" },
                            new { field = "eventstartdatetime" },
                            new { field = "eventtype" },
                            new { field = "timeZoneShortName" },
                            new { field = "epsestimate" },
                            new { field = "epsactual" },
                            new { field = "epssurprisepct" }
                        },
                        rows = new object[]
                        {
                            new object[]
                            {
                                1700000000,
                                1699990000,
                                2,
                                "EST",
                                1.23m,
                                1.1m,
                                0.1m
                            }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(response);
    }

    public static string BuildOptionChainResponse(string symbol)
    {
        var response = new
        {
            optionChain = new
            {
                result = new object[]
                {
                    new
                    {
                        underlyingSymbol = symbol,
                        expirationDates = new object[] { 1700000000, 1700600000 },
                        quote = new
                        {
                            symbol,
                            regularMarketPrice = 190.5m,
                            regularMarketChange = 1.2m,
                            regularMarketChangePercent = 0.63m,
                            currency = "USD",
                            exchange = "NMS",
                            quoteType = "EQUITY",
                            shortName = "Apple Inc.",
                            longName = "Apple Inc.",
                            exchangeTimezoneName = "America/New_York"
                        },
                        options = new object[]
                        {
                            new
                            {
                                expirationDate = 1700000000,
                                calls = new object[]
                                {
                                    new
                                    {
                                        contractSymbol = "AAPL240119C00190000",
                                        strike = 190m,
                                        lastPrice = 5.25m,
                                        bid = 5.1m,
                                        ask = 5.3m,
                                        change = 0.2m,
                                        percentChange = 3.96m,
                                        volume = 1200,
                                        openInterest = 5000,
                                        impliedVolatility = 0.25m,
                                        inTheMoney = true,
                                        contractSize = "REGULAR",
                                        currency = "USD",
                                        expiration = 1700000000,
                                        lastTradeDate = 1699900000
                                    }
                                },
                                puts = new object[]
                                {
                                    new
                                    {
                                        contractSymbol = "AAPL240119P00190000",
                                        strike = 190m,
                                        lastPrice = 4.1m,
                                        bid = 4.0m,
                                        ask = 4.2m,
                                        change = -0.1m,
                                        percentChange = -2.38m,
                                        volume = 900,
                                        openInterest = 4500,
                                        impliedVolatility = 0.27m,
                                        inTheMoney = false,
                                        contractSize = "REGULAR",
                                        currency = "USD",
                                        expiration = 1700000000,
                                        lastTradeDate = 1699900000
                                    }
                                }
                            }
                        }
                    }
                },
                error = (object?)null
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
