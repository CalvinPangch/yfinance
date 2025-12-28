using System;
using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Interfaces;
using YFinance.Interfaces.Utils;
using YFinance.Models.Requests;
using YFinance.Tests.TestFixtures;

namespace YFinance.Tests.Unit.Scrapers;

public class NewsScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly NewsScraper _scraper;

    public NewsScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _mockDataParser = new Mock<IDataParser>();
        _scraper = new NewsScraper(_mockClient.Object, _mockDataParser.Object);
    }

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new NewsScraper(null!, _mockDataParser.Object));
    }

    [Fact]
    public void Constructor_NullDataParser_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new NewsScraper(_mockClient.Object, null!));
    }

    [Fact]
    public async Task GetNewsAsync_ValidResponse_ReturnsNews()
    {
        var response = TestDataBuilder.BuildNewsResponse("AAPL");
        _mockDataParser.Setup(p => p.UnixTimestampToDateTime(It.IsAny<long>()))
            .Returns(DateTime.UnixEpoch);

        _mockClient.Setup(c => c.PostAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var request = new NewsRequest { Symbol = "AAPL", Count = 1 };
        var result = await _scraper.GetNewsAsync(request);

        result.Should().HaveCount(1);
        result[0].Title.Should().Be("News Title");
        result[0].RelatedTickers.Should().Contain("AAPL");
    }

    [Fact]
    public async Task GetNewsAsync_BuildsCorrectEndpoint()
    {
        var response = TestDataBuilder.BuildNewsResponse("AAPL");
        string? capturedEndpoint = null;

        _mockClient.Setup(c => c.PostAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, string, CancellationToken>((endpoint, _, _) =>
            {
                capturedEndpoint = endpoint;
            })
            .ReturnsAsync(response);

        var request = new NewsRequest { Symbol = "AAPL", Count = 5, Tab = "news" };
        await _scraper.GetNewsAsync(request);

        capturedEndpoint.Should().Contain("finance.yahoo.com/xhr/ncp");
        capturedEndpoint.Should().Contain("queryRef=latestNews");
    }
}
