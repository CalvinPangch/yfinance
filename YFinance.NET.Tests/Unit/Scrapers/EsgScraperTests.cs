using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.NET.Implementation.Scrapers;
using YFinance.NET.Implementation.Utils;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Tests.TestFixtures;

namespace YFinance.NET.Tests.Unit.Scrapers;

public class EsgScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly Mock<ISymbolValidator> _mockSymbolValidator;
    private readonly EsgScraper _scraper;

    public EsgScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _mockSymbolValidator = new Mock<ISymbolValidator>();
        _mockSymbolValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);
        _scraper = new EsgScraper(_mockClient.Object, new DataParser(), _mockSymbolValidator.Object);
    }

    [Fact]
    public async Task GetEsgAsync_ValidResponse_ReturnsData()
    {
        var response = TestDataBuilder.BuildEsgScoresResponse("AAPL");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetEsgAsync("AAPL");

        result.Symbol.Should().Be("AAPL");
        result.TotalEsg.Should().Be(25.1m);
        result.PeerGroup.Should().Be("Technology Hardware");
        result.PeerEsgScorePerformance.Should().Be("AVG_PERF");
        result.EnvironmentPercentile.Should().Be(7.0m);
    }
}
