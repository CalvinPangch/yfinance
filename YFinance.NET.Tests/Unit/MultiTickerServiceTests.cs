using Moq;
using Xunit;
using YFinance.NET.Implementation;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Enums;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Tests.Unit;

public class MultiTickerServiceTests
{
    [Fact]
    public async Task GetHistoryAsync_DelegatesToHistoryScraper()
    {
        // Arrange
        var historyScraper = new Mock<IHistoryScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var tickerService = new Mock<ITickerService>();
        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        historyScraper.Setup(s => s.GetHistoryAsync(It.IsAny<string>(), request, It.IsAny<CancellationToken>()))
            .ReturnsAsync((string symbol, HistoryRequest _, CancellationToken _) => new HistoricalData { Symbol = symbol });

        var service = new MultiTickerService(historyScraper.Object, newsScraper.Object, tickerService.Object);

        // Act
        var result = await service.GetHistoryAsync(new[] { "AAPL", "MSFT" }, request, maxConcurrency: 1);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("AAPL", result["AAPL"].Symbol);
        Assert.Equal("MSFT", result["MSFT"].Symbol);
        historyScraper.Verify(s => s.GetHistoryAsync("AAPL", request, It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.Verify(s => s.GetHistoryAsync("MSFT", request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetNewsAsync_DelegatesToNewsScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var tickerService = new Mock<ITickerService>();
        var expected = new List<NewsItem> { new NewsItem { Title = "News" } };

        newsScraper.Setup(s => s.GetNewsAsync(It.IsAny<NewsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new MultiTickerService(historyScraper.Object, newsScraper.Object, tickerService.Object);

        var result = await service.GetNewsAsync(new[] { "AAPL", "MSFT" }, count: 5, maxConcurrency: 1);

        Assert.Equal(2, result.Count);
        Assert.Same(expected, result["AAPL"]);
        Assert.Same(expected, result["MSFT"]);
        newsScraper.Verify(s => s.GetNewsAsync(It.IsAny<NewsRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
    }
}
