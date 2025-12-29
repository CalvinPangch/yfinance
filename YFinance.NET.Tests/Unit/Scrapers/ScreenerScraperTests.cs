using System;
using System.Text.Json;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.NET.Implementation.Scrapers;
using YFinance.NET.Interfaces;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;
using YFinance.NET.Tests.TestFixtures;

namespace YFinance.NET.Tests.Unit.Scrapers;

public class ScreenerScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly ScreenerScraper _scraper;

    public ScreenerScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new ScreenerScraper(_mockClient.Object);
    }

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ScreenerScraper(null!));
    }

    [Fact]
    public async Task ScreenAsync_Predefined_UsesPredefinedEndpoint()
    {
        var response = TestDataBuilder.BuildScreenerResponse("AAPL");
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

        var request = new ScreenerRequest { PredefinedId = "most_actives", Count = 10 };
        var result = await _scraper.ScreenAsync(request);

        capturedEndpoint.Should().Be("/v1/finance/screener/predefined/saved");
        capturedParams.Should().NotBeNull();
        capturedParams!["scrIds"].Should().Be("most_actives");
        capturedParams["count"].Should().Be("10");
        result.Quotes.Should().HaveCount(1);
    }

    [Fact]
    public async Task ScreenAsync_CustomQuery_PostsPayload()
    {
        var response = TestDataBuilder.BuildScreenerResponse("MSFT");
        string? capturedEndpoint = null;
        string? capturedBody = null;

        _mockClient.Setup(c => c.PostAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, string, CancellationToken>((endpoint, body, _) =>
            {
                capturedEndpoint = endpoint;
                capturedBody = body;
            })
            .ReturnsAsync(response);

        var request = new ScreenerRequest
        {
            Query = new EquityQuery("EQ", "region", "us"),
            SortField = "percentchange",
            SortAsc = true
        };

        var result = await _scraper.ScreenAsync(request);

        capturedEndpoint.Should().Be("/v1/finance/screener");
        capturedBody.Should().NotBeNull();

        var body = JsonDocument.Parse(capturedBody!).RootElement;
        body.GetProperty("quoteType").GetString().Should().Be("EQUITY");
        body.GetProperty("sortType").GetString().Should().Be("ASC");
        result.Quotes.Should().HaveCount(1);
    }

    [Fact]
    public async Task ScreenAsync_PredefinedWithOffset_UsesCustomEndpoint()
    {
        var response = TestDataBuilder.BuildScreenerResponse("AAPL");
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

        var request = new ScreenerRequest { PredefinedId = "most_actives", Offset = 10 };
        await _scraper.ScreenAsync(request);

        capturedEndpoint.Should().Be("/v1/finance/screener");
    }
}
