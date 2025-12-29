using System.Collections.Generic;
using Moq;
using Xunit;
using YFinance.NET.Implementation;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Tests.Unit;

public class TickerServiceTests
{
    [Fact]
    public async Task GetQuoteAsync_DelegatesToQuoteScraper()
    {
        // Arrange
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new QuoteData { Symbol = "AAPL" };

        quoteScraper
            .Setup(scraper => scraper.GetQuoteAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

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
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetFundsDataAsync_DelegatesToFundsScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new FundsData { Symbol = "VTI" };

        fundsScraper
            .Setup(scraper => scraper.GetFundsDataAsync("VTI", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

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
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetNewsAsync_DelegatesToNewsScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new List<NewsItem> { new NewsItem { Title = "News" } };
        var request = new NewsRequest { Symbol = "AAPL" };

        newsScraper
            .Setup(scraper => scraper.GetNewsAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

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
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetRecommendationsAsync_DelegatesToAnalysisScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new List<RecommendationTrendEntry> { new RecommendationTrendEntry { Period = "0m" } };

        analysisScraper
            .Setup(scraper => scraper.GetRecommendationsAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

        var result = await service.GetRecommendationsAsync("AAPL");

        Assert.Same(expected, result);
        analysisScraper.Verify(scraper => scraper.GetRecommendationsAsync("AAPL", It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetUpgradesDowngradesAsync_DelegatesToAnalysisScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new List<UpgradeDowngradeEntry> { new UpgradeDowngradeEntry { Firm = "Firm" } };

        analysisScraper
            .Setup(scraper => scraper.GetUpgradesDowngradesAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

        var result = await service.GetUpgradesDowngradesAsync("AAPL");

        Assert.Same(expected, result);
        analysisScraper.Verify(scraper => scraper.GetUpgradesDowngradesAsync("AAPL", It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetEsgAsync_DelegatesToEsgScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new EsgData { Symbol = "AAPL", TotalEsg = 25.1m };

        esgScraper
            .Setup(scraper => scraper.GetEsgAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

        var result = await service.GetEsgAsync("AAPL");

        Assert.Same(expected, result);
        esgScraper.Verify(scraper => scraper.GetEsgAsync("AAPL", It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetCalendarAsync_DelegatesToCalendarScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var expected = new CalendarData { Symbol = "AAPL" };

        calendarScraper
            .Setup(scraper => scraper.GetCalendarAsync("AAPL", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

        var result = await service.GetCalendarAsync("AAPL");

        Assert.Same(expected, result);
        calendarScraper.Verify(scraper => scraper.GetCalendarAsync("AAPL", It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetSharesHistoryAsync_DelegatesToSharesScraper()
    {
        var historyScraper = new Mock<IHistoryScraper>();
        var quoteScraper = new Mock<IQuoteScraper>();
        var infoScraper = new Mock<IInfoScraper>();
        var fastInfoScraper = new Mock<IFastInfoScraper>();
        var fundamentalsScraper = new Mock<IFundamentalsScraper>();
        var analysisScraper = new Mock<IAnalysisScraper>();
        var holdersScraper = new Mock<IHoldersScraper>();
        var fundsScraper = new Mock<IFundsScraper>();
        var newsScraper = new Mock<INewsScraper>();
        var earningsScraper = new Mock<IEarningsScraper>();
        var optionsScraper = new Mock<IOptionsScraper>();
        var esgScraper = new Mock<IEsgScraper>();
        var calendarScraper = new Mock<ICalendarScraper>();
        var sharesScraper = new Mock<ISharesScraper>();
        var isinService = new Mock<IIsinService>();
        var request = new SharesHistoryRequest { Symbol = "AAPL" };
        var expected = new SharesHistoryData { Symbol = "AAPL" };

        sharesScraper
            .Setup(scraper => scraper.GetSharesHistoryAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var service = new TickerService(
            historyScraper.Object,
            quoteScraper.Object,
            infoScraper.Object,
            fastInfoScraper.Object,
            fundamentalsScraper.Object,
            analysisScraper.Object,
            holdersScraper.Object,
            fundsScraper.Object,
            newsScraper.Object,
            earningsScraper.Object,
            optionsScraper.Object,
            esgScraper.Object,
            calendarScraper.Object,
            sharesScraper.Object,
            isinService.Object);

        var result = await service.GetSharesHistoryAsync(request);

        Assert.Same(expected, result);
        sharesScraper.Verify(scraper => scraper.GetSharesHistoryAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        historyScraper.VerifyNoOtherCalls();
        quoteScraper.VerifyNoOtherCalls();
        fundamentalsScraper.VerifyNoOtherCalls();
        analysisScraper.VerifyNoOtherCalls();
        holdersScraper.VerifyNoOtherCalls();
        fundsScraper.VerifyNoOtherCalls();
        newsScraper.VerifyNoOtherCalls();
        earningsScraper.VerifyNoOtherCalls();
        optionsScraper.VerifyNoOtherCalls();
        esgScraper.VerifyNoOtherCalls();
        calendarScraper.VerifyNoOtherCalls();
        sharesScraper.VerifyNoOtherCalls();
        infoScraper.VerifyNoOtherCalls();
        fastInfoScraper.VerifyNoOtherCalls();
        isinService.VerifyNoOtherCalls();
    }
}
