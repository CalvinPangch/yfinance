using Moq;
using Xunit;
using YFinance.Implementation;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Enums;
using YFinance.Models.Requests;

namespace YFinance.Tests.Unit;

public class MultiTickerServiceTests
{
    [Fact]
    public async Task GetHistoryAsync_DelegatesToHistoryScraper()
    {
        // Arrange
        var historyScraper = new Mock<IHistoryScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var request = new HistoryRequest { Period = Period.OneDay, Interval = Interval.OneDay };

        historyScraper.Setup(s => s.GetHistoryAsync(It.IsAny<string>(), request, It.IsAny<CancellationToken>()))
            .ReturnsAsync((string symbol, HistoryRequest _, CancellationToken _) => new HistoricalData { Symbol = symbol });

        var service = new MultiTickerService(historyScraper.Object, newsScraper.Object);

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
        var expected = new List<NewsItem> { new NewsItem { Title = "News" } };

        newsScraper.Setup(s => s.GetNewsAsync(It.IsAny<NewsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new MultiTickerService(historyScraper.Object, newsScraper.Object);

        var result = await service.GetNewsAsync(new[] { "AAPL", "MSFT" }, count: 5, maxConcurrency: 1);

        Assert.Equal(2, result.Count);
        Assert.Same(expected, result["AAPL"]);
        Assert.Same(expected, result["MSFT"]);
        newsScraper.Verify(s => s.GetNewsAsync(It.IsAny<NewsRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
    }
}
