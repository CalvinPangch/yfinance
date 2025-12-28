using Moq;
using Xunit;
using YFinance.Implementation;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;

namespace YFinance.Tests.Unit;

public class TickerServiceTests
{
    [Fact]
    public async Task GetQuoteAsync_DelegatesToQuoteScraper()
    {
        // Arrange
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var expected = new QuoteData { Symbol = "AAPL" };

        quoteScraper
            .Setup(scraper => scraper.GetQuoteAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object);

        // Act
        var result = await service.GetQuoteAsync("AAPL");

        // Assert
        Assert.Same(expected, result);
        quoteScraper.Verify(scraper => scraper.GetQuoteAsync("AAPL", It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
    }
}
