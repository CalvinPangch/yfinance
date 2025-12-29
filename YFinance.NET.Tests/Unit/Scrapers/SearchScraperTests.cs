using System;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.NET.Implementation.Scrapers;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models.Requests;
using YFinance.NET.Tests.TestFixtures;

namespace YFinance.NET.Tests.Unit.Scrapers;

public class SearchScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly SearchScraper _scraper;

    public SearchScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _mockDataParser = new Mock<IDataParser>();
        _scraper = new SearchScraper(_mockClient.Object, _mockDataParser.Object);
    }

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SearchScraper(null!, _mockDataParser.Object));
    }

    [Fact]
    public void Constructor_NullDataParser_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SearchScraper(_mockClient.Object, null!));
    }

    [Fact]
    public async Task SearchAsync_ValidResponse_ReturnsSearchResult()
    {
        var response = TestDataBuilder.BuildSearchResponse("AAPL");
        var request = new SearchRequest { Query = "Apple" };

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.SearchAsync(request);

        result.Quotes.Should().HaveCount(1);
        result.News.Should().HaveCount(1);
        result.Quotes[0].Symbol.Should().Be("AAPL");
        result.News[0].Title.Should().Be("Test News");
    }

    [Fact]
    public async Task SearchAsync_BuildsCorrectEndpointAndParams()
    {
        var response = TestDataBuilder.BuildSearchResponse("AAPL");
        string? capturedEndpoint = null;
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((endpoint, parameters, _) =>
            {
                capturedEndpoint = endpoint;
                capturedParams = parameters;
            })
            .ReturnsAsync(response);

        var request = new SearchRequest { Query = "Apple", QuotesCount = 5, NewsCount = 3 };
        await _scraper.SearchAsync(request);

        capturedEndpoint.Should().Contain("query2.finance.yahoo.com");
        capturedEndpoint.Should().Contain("/v1/finance/search");
        capturedParams.Should().NotBeNull();
        capturedParams!["q"].Should().Be("Apple");
        capturedParams["quotesCount"].Should().Be("5");
        capturedParams["newsCount"].Should().Be("3");
    }
}
