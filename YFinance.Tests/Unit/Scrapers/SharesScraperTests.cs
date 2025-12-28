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

public class SharesScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly SharesScraper _scraper;

    public SharesScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new SharesScraper(_mockClient.Object, new DataParser());
    }

    [Fact]
    public async Task GetSharesHistoryAsync_ValidResponse_ReturnsEntries()
    {
        var response = TestDataBuilder.BuildSharesTimeseriesResponse("AAPL");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var request = new SharesHistoryRequest { Symbol = "AAPL" };
        var result = await _scraper.GetSharesHistoryAsync(request);

        result.Symbol.Should().Be("AAPL");
        result.Entries.Should().ContainSingle();
        result.Entries[0].Date.Should().Be(DateTime.SpecifyKind(new DateTime(2024, 9, 30), DateTimeKind.Utc));
        result.Entries[0].SharesOutstanding.Should().Be(1000000m);
        result.Entries[0].FloatShares.Should().Be(800000m);
    }
}
