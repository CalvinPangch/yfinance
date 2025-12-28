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

public class CalendarScraperTests
{
    private readonly Mock<IYahooFinanceClient> _mockClient;
    private readonly CalendarScraper _scraper;

    public CalendarScraperTests()
    {
        _mockClient = new Mock<IYahooFinanceClient>();
        _scraper = new CalendarScraper(_mockClient.Object, new DataParser());
    }

    [Fact]
    public async Task GetCalendarAsync_ValidResponse_ReturnsCalendar()
    {
        var response = TestDataBuilder.BuildCalendarResponse("AAPL");
        _mockClient.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _scraper.GetCalendarAsync("AAPL");

        result.Symbol.Should().Be("AAPL");
        result.EarningsDates.Should().ContainSingle();
        result.EarningsAverage.Should().Be(1.2m);
        result.DividendAmount.Should().Be(0.25m);
        result.DividendDate.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1700500000).UtcDateTime);
        result.ExDividendDate.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1700400000).UtcDateTime);
        result.CapitalGainsAmount.Should().Be(0.5m);
    }
}
