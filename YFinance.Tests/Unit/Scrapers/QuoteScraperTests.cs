using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Interfaces;
using YFinance.Interfaces.Utils;

namespace YFinance.Tests.Unit.Scrapers;

public class QuoteScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly QuoteScraper _scraper;

    public QuoteScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _mockDataParser = new Mock<IDataParser>();
        _scraper = new QuoteScraper(_mockClient.Object, _mockDataParser.Object);

        // Setup default mock behaviors
        _mockDataParser.Setup(p => p.ExtractDecimal(It.IsAny<System.Text.Json.JsonElement>()))
            .Returns((decimal?)null);
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new QuoteScraper(null!, _mockDataParser.Object));
    }

    [Fact]
    public void Constructor_NullDataParser_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new QuoteScraper(_mockClient.Object, null!));
    }

    #endregion

    #region Input Validation Tests

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(string? symbol)
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _scraper.GetQuoteAsync(symbol!));
    }

    #endregion

    #region Quote Data Parsing Tests

    [Fact]
    public async Task GetQuoteAsync_ValidResponse_ReturnsQuoteData()
    {
        // Arrange
        var symbol = "AAPL";
        var response = BuildValidQuoteSummaryResponse(symbol);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        _mockDataParser.Setup(p => p.ExtractDecimal(It.IsAny<System.Text.Json.JsonElement>()))
            .Returns(150.25m);

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);
    }

    [Fact]
    public async Task GetQuoteAsync_BuildsCorrectEndpoint()
    {
        // Arrange
        var symbol = "MSFT";
        var response = BuildValidQuoteSummaryResponse(symbol);
        string? capturedEndpoint = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((endpoint, _, _) =>
                capturedEndpoint = endpoint)
            .ReturnsAsync(response);

        // Act
        await _scraper.GetQuoteAsync(symbol);

        // Assert
        capturedEndpoint.Should().Be($"/v10/finance/quoteSummary/{symbol}");
    }

    [Fact]
    public async Task GetQuoteAsync_IncludesCorrectModules()
    {
        // Arrange
        var symbol = "GOOGL";
        var response = BuildValidQuoteSummaryResponse(symbol);
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((_, params_, _) =>
                capturedParams = params_)
            .ReturnsAsync(response);

        // Act
        await _scraper.GetQuoteAsync(symbol);

        // Assert
        capturedParams.Should().NotBeNull();
        capturedParams!.Should().ContainKey("modules");
        capturedParams["modules"].Should().Contain("financialData");
        capturedParams["modules"].Should().Contain("quoteType");
        capturedParams["modules"].Should().Contain("defaultKeyStatistics");
        capturedParams["modules"].Should().Contain("assetProfile");
        capturedParams["modules"].Should().Contain("summaryDetail");
    }

    [Fact]
    public async Task GetQuoteAsync_EmptyResult_ReturnsQuoteDataWithSymbol()
    {
        // Arrange
        var symbol = "INVALID";
        var response = """{"quoteSummary":{"result":[]}}""";

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);
        result.ShortName.Should().BeEmpty();
        result.LongName.Should().BeEmpty();
    }

    [Fact]
    public async Task GetQuoteAsync_ParsesQuoteTypeModule()
    {
        // Arrange
        var symbol = "AAPL";
        var response = """
        {
            "quoteSummary": {
                "result": [{
                    "quoteType": {
                        "shortName": "Apple Inc.",
                        "longName": "Apple Inc.",
                        "exchange": "NMS",
                        "quoteType": "EQUITY",
                        "timeZoneFullName": "America/New_York"
                    }
                }]
            }
        }
        """;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.ShortName.Should().Be("Apple Inc.");
        result.LongName.Should().Be("Apple Inc.");
        result.Exchange.Should().Be("NMS");
        result.QuoteType.Should().Be("EQUITY");
        result.TimeZone.Should().Be("America/New_York");
    }

    [Fact]
    public async Task GetQuoteAsync_ParsesSummaryDetailModule()
    {
        // Arrange
        var symbol = "AAPL";
        var response = """
        {
            "quoteSummary": {
                "result": [{
                    "summaryDetail": {
                        "open": {"raw": 150.5},
                        "dayHigh": {"raw": 152.0},
                        "dayLow": {"raw": 149.5},
                        "previousClose": {"raw": 151.0},
                        "volume": {"raw": 50000000},
                        "currency": "USD"
                    }
                }]
            }
        }
        """;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        _mockDataParser.Setup(p => p.ExtractDecimal(It.IsAny<System.Text.Json.JsonElement>()))
            .Returns<System.Text.Json.JsonElement>(element =>
            {
                if (element.TryGetProperty("raw", out var raw) && raw.ValueKind == System.Text.Json.JsonValueKind.Number)
                    return raw.GetDecimal();
                return null;
            });

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.RegularMarketOpen.Should().Be(150.5m);
        result.RegularMarketDayHigh.Should().Be(152.0m);
        result.RegularMarketDayLow.Should().Be(149.5m);
        result.RegularMarketPreviousClose.Should().Be(151.0m);
        result.RegularMarketVolume.Should().Be(50000000);
        result.Currency.Should().Be("USD");
    }

    [Fact]
    public async Task GetQuoteAsync_ParsesFinancialDataModule()
    {
        // Arrange
        var symbol = "AAPL";
        var response = """
        {
            "quoteSummary": {
                "result": [{
                    "financialData": {
                        "currentPrice": {"raw": 150.25}
                    }
                }]
            }
        }
        """;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        _mockDataParser.Setup(p => p.ExtractDecimal(It.IsAny<System.Text.Json.JsonElement>()))
            .Returns<System.Text.Json.JsonElement>(element =>
            {
                if (element.TryGetProperty("raw", out var raw) && raw.ValueKind == System.Text.Json.JsonValueKind.Number)
                    return raw.GetDecimal();
                return null;
            });

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.RegularMarketPrice.Should().Be(150.25m);
    }

    [Fact]
    public async Task GetQuoteAsync_ParsesDefaultKeyStatisticsModule()
    {
        // Arrange
        var symbol = "AAPL";
        var response = """
        {
            "quoteSummary": {
                "result": [{
                    "defaultKeyStatistics": {
                        "trailingPE": {"raw": 28.5},
                        "forwardPE": {"raw": 25.0},
                        "pegRatio": {"raw": 2.1},
                        "priceToBook": {"raw": 35.0},
                        "trailingEps": {"raw": 5.5},
                        "marketCap": {"raw": 2500000000000}
                    }
                }]
            }
        }
        """;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        _mockDataParser.Setup(p => p.ExtractDecimal(It.IsAny<System.Text.Json.JsonElement>()))
            .Returns<System.Text.Json.JsonElement>(element =>
            {
                if (element.TryGetProperty("raw", out var raw) && raw.ValueKind == System.Text.Json.JsonValueKind.Number)
                    return raw.GetDecimal();
                return null;
            });

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.PeRatio.Should().Be(28.5m);
        result.ForwardPE.Should().Be(25.0m);
        result.PegRatio.Should().Be(2.1m);
        result.PriceToBook.Should().Be(35.0m);
        result.EarningsPerShare.Should().Be(5.5m);
        result.MarketCap.Should().Be(2500000000000m);
    }

    [Fact]
    public async Task GetQuoteAsync_MissingModules_HandlesGracefully()
    {
        // Arrange
        var symbol = "TEST";
        var response = """
        {
            "quoteSummary": {
                "result": [{}]
            }
        }
        """;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);
        result.RegularMarketPrice.Should().BeNull();
        result.ShortName.Should().BeEmpty();
    }

    [Fact]
    public async Task GetQuoteAsync_NullValues_HandlesGracefully()
    {
        // Arrange
        var symbol = "TEST";
        var response = """
        {
            "quoteSummary": {
                "result": [{
                    "summaryDetail": {
                        "open": null,
                        "dayHigh": null,
                        "volume": null
                    }
                }]
            }
        }
        """;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _scraper.GetQuoteAsync(symbol);

        // Assert
        result.Should().NotBeNull();
        result.RegularMarketOpen.Should().BeNull();
        result.RegularMarketDayHigh.Should().BeNull();
        result.RegularMarketVolume.Should().BeNull();
    }

    #endregion

    #region Cancellation Token Tests

    [Fact]
    public async Task GetQuoteAsync_CancellationRequested_PropagatesToken()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        cts.Cancel();

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            () => _scraper.GetQuoteAsync("AAPL", cts.Token));
    }

    #endregion

    #region Helper Methods

    private static string BuildValidQuoteSummaryResponse(string symbol)
    {
        return $$"""
        {
            "quoteSummary": {
                "result": [{
                    "quoteType": {
                        "shortName": "{{symbol}} Inc.",
                        "longName": "{{symbol}} Incorporated",
                        "exchange": "NMS",
                        "quoteType": "EQUITY"
                    },
                    "summaryDetail": {
                        "open": {"raw": 150.0},
                        "currency": "USD"
                    },
                    "financialData": {
                        "currentPrice": {"raw": 150.25}
                    },
                    "defaultKeyStatistics": {
                        "trailingPE": {"raw": 28.5}
                    }
                }]
            }
        }
        """;
    }

    #endregion
}
