using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Interfaces;
using YFinance.Interfaces.Utils;
using YFinance.Implementation.Utils;
using YFinance.Models.Enums;
using YFinance.Models.Requests;
using YFinance.Tests.TestFixtures;

namespace YFinance.Tests.Unit.Scrapers;

public class HistoryScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly Mock<IPriceRepair> _mockPriceRepair;
    private readonly Mock<ITimezoneHelper> _mockTimezoneHelper;
    private readonly HistoryScraper _scraper;

    public HistoryScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _mockDataParser = new Mock<IDataParser>();
        _mockPriceRepair = new Mock<IPriceRepair>();
        _mockTimezoneHelper = new Mock<ITimezoneHelper>();

        _mockTimezoneHelper
            .Setup(t => t.FixDstIssues(It.IsAny<DateTime[]>(), It.IsAny<string>()))
            .Returns<DateTime[], string>((timestamps, _) => timestamps);

        _mockPriceRepair
            .Setup(p => p.RepairPrices(It.IsAny<decimal[]>(), It.IsAny<DateTime[]>(), It.IsAny<Dictionary<DateTime, decimal>?>()))
            .Returns<decimal[], DateTime[], Dictionary<DateTime, decimal>?>((prices, _, _) => prices);

        _scraper = new HistoryScraper(
            _mockClient.Object,
            _mockDataParser.Object,
            _mockPriceRepair.Object,
            _mockTimezoneHelper.Object);
    }

    #region Happy Path Tests

    [Fact]
    public async Task GetHistoryAsync_ValidResponse_ParsesCorrectly()
    {
        // Arrange
        var symbol = "AAPL";
        var bars = new[]
        {
            new PriceBar(new DateTime(2024, 1, 1), 150m, 155m, 149m, 153m, 1000000)
        };
        var response = TestDataBuilder.BuildValidChartResponse(symbol, bars);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest
        {
            Period = Period.OneDay,
            Interval = Interval.OneDay
        };

        // Act
        var result = await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);
        result.Timestamps.Should().NotBeEmpty();
        _mockClient.Verify(c => c.GetAsync(
            $"/v8/finance/chart/{symbol}",
            It.IsAny<Dictionary<string, string>>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetHistoryAsync_WithDividends_IncludesDividendData()
    {
        // Arrange
        var symbol = "MSFT";
        var divDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var bars = new[]
        {
            new PriceBar(divDate, 100m, 105m, 99m, 103m, 500000)
        };
        var dividends = new Dictionary<DateTime, decimal>
        {
            { divDate, 0.75m }
        };

        var response = TestDataBuilder.BuildChartResponseWithEvents(symbol, bars, dividends);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.OneMonth, Interval = Interval.OneDay };

        // Act
        var result = await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Should().NotBeNull();
        result.Dividends.Should().ContainKey(divDate);
        result.Dividends[divDate].Should().Be(0.75m);
    }

    [Fact]
    public async Task GetHistoryAsync_WithStockSplits_IncludesSplitData()
    {
        // Arrange
        var symbol = "TSLA";
        var splitDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var bars = new[]
        {
            new PriceBar(splitDate, 200m, 210m, 195m, 205m, 2000000)
        };
        var splits = new Dictionary<DateTime, (decimal numerator, decimal denominator)>
        {
            { splitDate, (3m, 1m) } // 3-for-1 split
        };

        var response = TestDataBuilder.BuildChartResponseWithEvents(symbol, bars, null, splits);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.OneYear, Interval = Interval.OneDay };

        // Act
        var result = await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Should().NotBeNull();
        result.StockSplits.Should().ContainKey(splitDate);
        result.StockSplits[splitDate].Should().Be(3m); // numerator / denominator
    }

    [Fact]
    public async Task GetHistoryAsync_WithoutAdjustedClose_UsesRegularClose()
    {
        // Arrange
        var symbol = "GOOGL";
        var bars = new[]
        {
            new PriceBar(new DateTime(2024, 1, 1), 140m, 145m, 138m, 143m, 800000)
        };
        var response = TestDataBuilder.BuildChartResponseWithoutAdjClose(symbol, bars);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.FiveDays, Interval = Interval.OneDay };

        // Act
        var result = await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Should().NotBeNull();
        // When no adjusted close, it should use regular close
        result.AdjustedClose.Should().Equal(result.Close);
    }

    [Fact]
    public async Task GetHistoryAsync_AutoAdjustWithoutAdjClose_UsesCorporateActions()
    {
        // Arrange
        var symbol = "SPLIT";
        var splitDate = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);
        var bars = new[]
        {
            new PriceBar(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), 100m, 100m, 100m, 100m, 1000),
            new PriceBar(splitDate, 50m, 50m, 50m, 50m, 1000)
        };
        var splits = new Dictionary<DateTime, (decimal numerator, decimal denominator)>
        {
            { splitDate, (2m, 1m) }
        };

        var response = TestDataBuilder.BuildChartResponseWithEventsWithoutAdjClose(symbol, bars, null, splits);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var realScraper = new HistoryScraper(
            _mockClient.Object,
            new DataParser(),
            new PriceRepair(),
            new TimezoneHelper());

        var request = new HistoryRequest { Period = Period.OneMonth, Interval = Interval.OneDay, AutoAdjust = true };

        // Act
        var result = await realScraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Close.Length.Should().Be(2);
        result.Close[0].Should().BeApproximately(50m, 0.01m);
        result.Close[1].Should().BeApproximately(50m, 0.01m);
    }

    [Fact]
    public async Task GetHistoryAsync_WeeklyInterval_ResamplesToWeeklyBars()
    {
        // Arrange
        var symbol = "WEEKLY";
        var bars = new[]
        {
            new PriceBar(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), 100m, 110m, 95m, 105m, 1000),
            new PriceBar(new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc), 106m, 112m, 104m, 110m, 2000),
            new PriceBar(new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc), 111m, 120m, 108m, 118m, 1500)
        };

        var response = TestDataBuilder.BuildValidChartResponse(symbol, bars);
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var realScraper = new HistoryScraper(
            _mockClient.Object,
            new DataParser(),
            new PriceRepair(),
            new TimezoneHelper());

        var request = new HistoryRequest { Period = Period.OneMonth, Interval = Interval.OneWeek };

        // Act
        var result = await realScraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Timestamps.Length.Should().Be(2);
        result.Open.Length.Should().Be(2);
        result.Volume.Length.Should().Be(2);
        result.High[0].Should().Be(112m);
        result.Low[0].Should().Be(95m);
        result.Close[0].Should().Be(110m);
        result.Volume[0].Should().Be(3000);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public async Task GetHistoryAsync_EmptyResponse_ReturnsEmptyData()
    {
        // Arrange
        var symbol = "EMPTY";
        var response = TestDataBuilder.BuildEmptyChartResponse(symbol);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        // Act
        var result = await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);
        result.Timestamps.Should().BeEmpty();
        result.Open.Should().BeEmpty();
        result.High.Should().BeEmpty();
    }

    [Fact]
    public async Task GetHistoryAsync_MissingTimezone_DefaultsToUTC()
    {
        // Arrange
        var symbol = "TEST";
        var response = TestDataBuilder.BuildEmptyChartResponse(symbol);

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        // Act
        var result = await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        result.TimeZone.Should().Be("UTC");
    }

    #endregion

    #region Input Validation

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(string? invalidSymbol)
    {
        // Arrange
        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _scraper.GetHistoryAsync(invalidSymbol!, request));
    }

    [Fact]
    public async Task GetHistoryAsync_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _scraper.GetHistoryAsync("AAPL", null!));
    }

    [Fact]
    public async Task GetHistoryAsync_InvalidRequest_ThrowsArgumentException()
    {
        // Arrange - request with neither Period nor Start/End
        var request = new HistoryRequest { Interval = Interval.OneDay };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _scraper.GetHistoryAsync("AAPL", request));
    }

    #endregion

    #region Query Parameter Building

    [Theory]
    [InlineData(Period.OneDay, "1d")]
    [InlineData(Period.FiveDays, "5d")]
    [InlineData(Period.OneMonth, "1mo")]
    [InlineData(Period.ThreeMonths, "3mo")]
    [InlineData(Period.SixMonths, "6mo")]
    [InlineData(Period.OneYear, "1y")]
    [InlineData(Period.TwoYears, "2y")]
    [InlineData(Period.FiveYears, "5y")]
    [InlineData(Period.TenYears, "10y")]
    [InlineData(Period.YearToDate, "ytd")]
    [InlineData(Period.Max, "max")]
    public async Task GetHistoryAsync_VariousPeriods_BuildsCorrectQueryParam(Period period, string expectedRange)
    {
        // Arrange
        var symbol = "AAPL";
        var response = TestDataBuilder.BuildEmptyChartResponse(symbol);
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((_, p, _) => capturedParams = p)
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = period, Interval = Interval.OneDay };

        // Act
        await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        capturedParams.Should().NotBeNull();
        capturedParams!["range"].Should().Be(expectedRange);
    }

    [Theory]
    [InlineData(Interval.OneMinute, "1m")]
    [InlineData(Interval.TwoMinutes, "2m")]
    [InlineData(Interval.FiveMinutes, "5m")]
    [InlineData(Interval.FifteenMinutes, "15m")]
    [InlineData(Interval.ThirtyMinutes, "30m")]
    [InlineData(Interval.SixtyMinutes, "60m")]
    [InlineData(Interval.NinetyMinutes, "90m")]
    [InlineData(Interval.OneHour, "1h")]
    [InlineData(Interval.OneDay, "1d")]
    [InlineData(Interval.FiveDays, "5d")]
    [InlineData(Interval.OneWeek, "1wk")]
    [InlineData(Interval.OneMonth, "1mo")]
    [InlineData(Interval.ThreeMonths, "3mo")]
    public async Task GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(Interval interval, string expectedInterval)
    {
        // Arrange
        var symbol = "AAPL";
        var response = TestDataBuilder.BuildEmptyChartResponse(symbol);
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((_, p, _) => capturedParams = p)
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.OneDay, Interval = interval };

        // Act
        await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        capturedParams.Should().NotBeNull();
        capturedParams!["interval"].Should().Be(expectedInterval);
    }

    [Fact]
    public async Task GetHistoryAsync_WithStartEndDates_BuildsUnixTimestamps()
    {
        // Arrange
        var symbol = "AAPL";
        var startDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var endDate = new DateTime(2024, 1, 31, 23, 59, 59, DateTimeKind.Utc);
        var response = TestDataBuilder.BuildEmptyChartResponse(symbol);
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((_, p, _) => capturedParams = p)
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest
        {
            Start = startDate,
            End = endDate,
            Interval = Interval.OneDay
        };

        // Act
        await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        capturedParams.Should().NotBeNull();
        capturedParams!.Should().ContainKey("period1");
        capturedParams.Should().ContainKey("period2");
        capturedParams.Should().NotContainKey("range");

        var expectedStart = ((DateTimeOffset)startDate).ToUnixTimeSeconds().ToString();
        var expectedEnd = ((DateTimeOffset)endDate).ToUnixTimeSeconds().ToString();
        capturedParams!["period1"].Should().Be(expectedStart);
        capturedParams!["period2"].Should().Be(expectedEnd);
    }

    [Fact]
    public async Task GetHistoryAsync_AlwaysIncludesDividendsAndSplits()
    {
        // Arrange
        var symbol = "AAPL";
        var response = TestDataBuilder.BuildEmptyChartResponse(symbol);
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((_, p, _) => capturedParams = p)
            .ReturnsAsync(response);

        SetupDataParserMocks();

        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        // Act
        await _scraper.GetHistoryAsync(symbol, request);

        // Assert
        capturedParams.Should().NotBeNull();
        capturedParams!["events"].Should().Be("div,split");
        capturedParams["includePrePost"].Should().Be("false");
    }

    #endregion

    #region Cancellation Token

    [Fact]
    public async Task GetHistoryAsync_CancellationRequested_PropagatesToken()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        cts.Cancel();

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            () => _scraper.GetHistoryAsync("AAPL", request, cts.Token));
    }

    #endregion

    #region Constructor Validation

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => new HistoryScraper(null!, _mockDataParser.Object, _mockPriceRepair.Object, _mockTimezoneHelper.Object));
    }

    [Fact]
    public void Constructor_NullDataParser_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => new HistoryScraper(_mockClient.Object, null!, _mockPriceRepair.Object, _mockTimezoneHelper.Object));
    }

    #endregion

    #region Helper Methods

    private void SetupDataParserMocks()
    {
        // Setup ParseDecimalArray to return empty arrays by default
        _mockDataParser.Setup(p => p.ParseDecimalArray(It.IsAny<System.Text.Json.JsonElement>(), It.IsAny<string>()))
            .Returns(Array.Empty<decimal>());

        // Setup ParseLongArray to return empty arrays by default
        _mockDataParser.Setup(p => p.ParseLongArray(It.IsAny<System.Text.Json.JsonElement>(), It.IsAny<string>()))
            .Returns(Array.Empty<long>());
    }

    #endregion
}
