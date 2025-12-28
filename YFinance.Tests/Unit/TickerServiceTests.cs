using System.Collections.Generic;
using Moq;
using Xunit;
using YFinance.Implementation;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Requests;

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
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var expected = new QuoteData { Symbol = "AAPL" };

        quoteScraper
            .Setup(scraper => scraper.GetQuoteAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object);

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
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetFundsDataAsync_DelegatesToFundsScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var expected = new FundsData { Symbol = "VTI" };

        fundsScraper
            .Setup(scraper => scraper.GetFundsDataAsync("VTI", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object);

        var result = await service.GetFundsDataAsync("VTI");

        Assert.Same(expected, result);
        fundsScraper.Verify(scraper => scraper.GetFundsDataAsync("VTI", It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetNewsAsync_DelegatesToNewsScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var expected = new List<NewsItem> { new NewsItem { Title = "News" } };
        var request = new NewsRequest { Symbol = "AAPL" };

        newsScraper
            .Setup(scraper => scraper.GetNewsAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object);

        var result = await service.GetNewsAsync(request);

        Assert.Same(expected, result);
        newsScraper.Verify(scraper => scraper.GetNewsAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
    }
}
