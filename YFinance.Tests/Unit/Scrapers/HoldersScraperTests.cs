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

public class HoldersScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly HoldersScraper _scraper;

    public HoldersScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new HoldersScraper(_mockClient.Object, new DataParser());
    }

    [Fact]
    public async Task GetHoldersAsync_ValidResponse_ReturnsHoldersData()
    {
        var response = TestDataBuilder.BuildHoldersResponse("AAPL");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetHoldersAsync("AAPL");

        result.Symbol.Should().Be("AAPL");
        result.InsidersPercentHeld.Should().Be(0.12m);
        result.InstitutionalHolders.Should().NotBeNull();
        result.InstitutionalHolders!.Should().ContainSingle();
        result.InsiderTransactions.Should().NotBeNull();
        result.InsiderTransactions!.Should().ContainSingle();
        var insiderHolders = result.InsiderHolders;
        insiderHolders.Should().NotBeNull();
        insiderHolders!.Should().ContainSingle();
        var fundHolders = result.FundHolders;
        fundHolders.Should().NotBeNull();
        fundHolders!.Should().ContainSingle();
        insiderHolders![0].Name.Should().Be("Insider Holder");
        fundHolders![0].Holder.Should().Be("Fund A");
    }
}
