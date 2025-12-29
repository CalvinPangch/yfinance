using System;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.NET.Implementation.Scrapers;
using YFinance.NET.Interfaces;
using YFinance.NET.Models.Enums;
using YFinance.NET.Models.Requests;
using YFinance.NET.Tests.TestFixtures;

namespace YFinance.NET.Tests.Unit.Scrapers;

public class LookupScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly LookupScraper _scraper;

    public LookupScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new LookupScraper(_mockClient.Object);
    }

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new LookupScraper(null!));
    }

    [Fact]
    public async Task LookupAsync_ValidResponse_ReturnsDocuments()
    {
        var response = TestDataBuilder.BuildLookupResponse("AAPL");
        var request = new LookupRequest { Query = "Apple", Type = LookupType.Equity };

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.LookupAsync(request);

        result.Documents.Should().HaveCount(1);
        result.Documents[0].Symbol.Should().Be("AAPL");
    }

    [Fact]
    public async Task LookupAsync_BuildsCorrectEndpointAndParams()
    {
        var response = TestDataBuilder.BuildLookupResponse("MSFT");
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

        var request = new LookupRequest { Query = "Microsoft", Type = LookupType.Equity, Count = 5 };
        await _scraper.LookupAsync(request);

        capturedEndpoint.Should().Be("/v1/finance/lookup");
        capturedParams.Should().NotBeNull();
        capturedParams!["query"].Should().Be("Microsoft");
        capturedParams["type"].Should().Be("equity");
        capturedParams["count"].Should().Be("5");
    }
}
