using System;
using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Implementation.Utils;
using YFinance.Interfaces;
using YFinance.Tests.TestFixtures;

namespace YFinance.Tests.Unit.Scrapers;

public class AnalysisScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly AnalysisScraper _scraper;

    public AnalysisScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new AnalysisScraper(_mockClient.Object, new DataParser());
    }

    [Fact]
    public async Task GetRecommendationsAsync_ValidResponse_ReturnsTrend()
    {
        var response = TestDataBuilder.BuildRecommendationTrendResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetRecommendationsAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Period.Should().Be("0m");
        result[0].StrongBuy.Should().Be(5);
    }

    [Fact]
    public async Task GetUpgradesDowngradesAsync_ValidResponse_ReturnsHistory()
    {
        var response = TestDataBuilder.BuildUpgradeDowngradeResponse();
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetUpgradesDowngradesAsync("AAPL");

        result.Should().NotBeEmpty();
        result[0].Firm.Should().Be("Firm");
        result[0].GradeDate.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1700000000).UtcDateTime);
        result[0].Action.Should().Be("up");
    }
}
