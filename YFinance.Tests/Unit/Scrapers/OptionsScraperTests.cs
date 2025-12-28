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

public class OptionsScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly OptionsScraper _scraper;

    public OptionsScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new OptionsScraper(_mockClient.Object, new DataParser());
    }

    [Fact]
    public async Task GetExpirationsAsync_ValidResponse_ReturnsDates()
    {
        var response = TestDataBuilder.BuildOptionChainResponse("AAPL");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetExpirationsAsync("AAPL");

        result.Should().HaveCount(2);
        result[0].Should().Be(DateTimeOffset.FromUnixTimeSeconds(1700000000).UtcDateTime);
    }

    [Fact]
    public async Task GetOptionChainAsync_ValidResponse_ReturnsChain()
    {
        var response = TestDataBuilder.BuildOptionChainResponse("AAPL");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var request = new OptionChainRequest { Symbol = "AAPL" };
        var result = await _scraper.GetOptionChainAsync(request);

        result.Symbol.Should().Be("AAPL");
        result.ExpirationDate.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1700000000).UtcDateTime);
        result.Calls.Should().HaveCount(1);
        result.Puts.Should().HaveCount(1);
        result.Underlying.Should().NotBeNull();
        result.Underlying!.RegularMarketPrice.Should().Be(190.5m);
        result.Calls[0].ContractSymbol.Should().Be("AAPL240119C00190000");
        result.Puts[0].InTheMoney.Should().BeFalse();
    }

    [Fact]
    public async Task GetOptionChainAsync_WithExpirationDate_UsesDateQueryParam()
    {
        var response = TestDataBuilder.BuildOptionChainResponse("AAPL");
        Dictionary<string, string>? capturedParams = null;

        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, Dictionary<string, string>?, CancellationToken>((_, query, _) =>
            {
                capturedParams = query;
            })
            .ReturnsAsync(response);

        var request = new OptionChainRequest
        {
            Symbol = "AAPL",
            ExpirationDate = DateTimeOffset.FromUnixTimeSeconds(1700000000).UtcDateTime
        };

        await _scraper.GetOptionChainAsync(request);

        capturedParams.Should().NotBeNull();
        capturedParams!["date"].Should().Be("1700000000");
    }
}
