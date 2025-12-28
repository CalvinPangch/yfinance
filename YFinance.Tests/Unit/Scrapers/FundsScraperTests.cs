using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Interfaces;
using YFinance.Interfaces.Utils;
using YFinance.Tests.TestFixtures;

namespace YFinance.Tests.Unit.Scrapers;

public class FundsScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly FundsScraper _scraper;

    public FundsScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _mockDataParser = new Mock<IDataParser>();
        _scraper = new FundsScraper(_mockClient.Object, _mockDataParser.Object);

        _mockDataParser.Setup(p => p.ExtractDecimal(It.IsAny<System.Text.Json.JsonElement>()))
            .Returns((System.Text.Json.JsonElement element) =>
                element.ValueKind == System.Text.Json.JsonValueKind.Number
                    ? element.GetDecimal()
                    : element.TryGetProperty("raw", out var raw) && raw.ValueKind == System.Text.Json.JsonValueKind.Number
                        ? raw.GetDecimal()
                        : (decimal?)null);
    }

    [Fact]
    public void Constructor_NullClient_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FundsScraper(null!, _mockDataParser.Object));
    }

    [Fact]
    public void Constructor_NullDataParser_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FundsScraper(_mockClient.Object, null!));
    }

    [Fact]
    public async Task GetFundsDataAsync_ValidResponse_ParsesData()
    {
        var response = TestDataBuilder.BuildFundsResponse("VTI");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetFundsDataAsync("VTI");

        result.QuoteType.Should().Be("ETF");
        result.Description.Should().Be("Fund description");
        result.Overview.Should().NotBeNull();
        result.Overview!.CategoryName.Should().Be("Large Blend");
        result.TopHoldings.Should().HaveCount(1);
        result.AssetClasses.Should().ContainKey("stockPosition");
        result.EquityHoldings.Should().NotBeEmpty();
        result.BondRatings.Should().ContainKey("AAA");
        result.SectorWeightings.Should().ContainKey("Technology");
    }
}
