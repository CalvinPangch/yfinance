using System;
using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Implementation.Utils;
using YFinance.Interfaces;
using YFinance.Models.Requests;
using YFinance.Tests.TestFixtures;

namespace YFinance.Tests.Unit.Scrapers;

public class EarningsScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly EarningsScraper _scraper;

    public EarningsScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new EarningsScraper(_mockClient.Object, new DataParser());
    }

    [Fact]
    public async Task GetEarningsEstimateAsync_ValidResponse_ReturnsEstimates()
    {
        var response = TestDataBuilder.BuildEarningsSummaryResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetEarningsEstimateAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Period.Should().Be("0q");
        result[0].Metrics.Should().ContainKey("avg");
        result[0].Metrics["avg"].Should().Be(1.2m);
    }

    [Fact]
    public async Task GetRevenueEstimateAsync_ValidResponse_ReturnsEstimates()
    {
        var response = TestDataBuilder.BuildEarningsSummaryResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetRevenueEstimateAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Metrics.Should().ContainKey("avg");
        result[0].Metrics["avg"].Should().Be(1000m);
    }

    [Fact]
    public async Task GetEpsTrendAsync_ValidResponse_ReturnsTrend()
    {
        var response = TestDataBuilder.BuildEarningsSummaryResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetEpsTrendAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Metrics.Should().ContainKey("current");
        result[0].Metrics["current"].Should().Be(1.1m);
        result[0].Metrics.Should().ContainKey("sevenDaysAgo");
        result[0].Metrics["sevenDaysAgo"].Should().Be(1.0m);
    }

    [Fact]
    public async Task GetEpsRevisionsAsync_ValidResponse_ReturnsRevisions()
    {
        var response = TestDataBuilder.BuildEarningsSummaryResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetEpsRevisionsAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Metrics.Should().ContainKey("upLast7days");
        result[0].Metrics["upLast7days"].Should().Be(2m);
        result[0].Metrics.Should().ContainKey("downLast30days");
        result[0].Metrics["downLast30days"].Should().Be(1m);
    }

    [Fact]
    public async Task GetEarningsHistoryAsync_ValidResponse_ReturnsHistory()
    {
        var response = TestDataBuilder.BuildEarningsSummaryResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetEarningsHistoryAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Quarter.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1704067200).UtcDateTime);
        result[0].Metrics.Should().ContainKey("epsActual");
        result[0].Metrics["epsActual"].Should().Be(1.05m);
    }

    [Fact]
    public async Task GetGrowthEstimatesAsync_ValidResponse_ReturnsGrowth()
    {
        var response = TestDataBuilder.BuildEarningsSummaryResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetGrowthEstimatesAsync("AAPL");

        result.Should().NotBeEmpty();
        var entry = result.Should().ContainSingle(e => e.Period == "0q").Subject;
        entry.StockTrend.Should().Be(0.1m);
        entry.IndustryTrend.Should().Be(0.2m);
        entry.SectorTrend.Should().Be(0.3m);
        entry.IndexTrend.Should().Be(0.4m);
    }

    [Fact]
    public async Task GetEarningsDatesAsync_ValidResponse_ReturnsDates()
    {
        var response = TestDataBuilder.BuildEarningsDatesResponse();
        _mockClient.Setup(c => c.PostAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var request = new EarningsDatesRequest { Symbol = "AAPL", Limit = 1 };
        var result = await _scraper.GetEarningsDatesAsync(request);

        result.Should().NotBeEmpty();
        result[0].EarningsDate.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1700000000).UtcDateTime);
        result[0].EventType.Should().Be("Earnings");
        result[0].EpsEstimate.Should().Be(1.23m);
        result[0].ReportedEps.Should().Be(1.1m);
    }
}
