using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;
using YFinance.NET.Models.Enums;

namespace YFinance.NET.Implementation;

/// <summary>
/// Main service for ticker operations.
/// Orchestrates calls to specialized scrapers for different data types.
/// </summary>
public class TickerService : ITickerService
{
    private readonly IHistoryScraper _historyScraper;
    private readonly IQuoteScraper _quoteScraper;
    private readonly IInfoScraper _infoScraper;
    private readonly IFastInfoScraper _fastInfoScraper;
    private readonly IFundamentalsScraper _fundamentalsScraper;
    private readonly IAnalysisScraper _analysisScraper;
    private readonly IHoldersScraper _holdersScraper;
    private readonly IFundsScraper _fundsScraper;
    private readonly INewsScraper _newsScraper;
    private readonly IEarningsScraper _earningsScraper;
    private readonly IOptionsScraper _optionsScraper;
    private readonly IEsgScraper _esgScraper;
    private readonly ICalendarScraper _calendarScraper;
    private readonly ISharesScraper _sharesScraper;
    private readonly IIsinService _isinService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TickerService"/> class.
    /// </summary>
    /// <param name="historyScraper">Scraper for historical price data.</param>
    /// <param name="quoteScraper">Scraper for quote data.</param>
    /// <param name="infoScraper">Scraper for ticker information.</param>
    /// <param name="fastInfoScraper">Scraper for fast info data.</param>
    /// <param name="fundamentalsScraper">Scraper for financial statements.</param>
    /// <param name="analysisScraper">Scraper for analyst data and recommendations.</param>
    /// <param name="holdersScraper">Scraper for holder information.</param>
    /// <param name="fundsScraper">Scraper for fund data.</param>
    /// <param name="newsScraper">Scraper for news data.</param>
    /// <param name="earningsScraper">Scraper for earnings and estimates.</param>
    /// <param name="optionsScraper">Scraper for options data.</param>
    /// <param name="esgScraper">Scraper for ESG data.</param>
    /// <param name="calendarScraper">Scraper for calendar events.</param>
    /// <param name="sharesScraper">Scraper for shares history data.</param>
    /// <param name="isinService">Service for ISIN code resolution.</param>
    public TickerService(
        IHistoryScraper historyScraper,
        IQuoteScraper quoteScraper,
        IInfoScraper infoScraper,
        IFastInfoScraper fastInfoScraper,
        IFundamentalsScraper fundamentalsScraper,
        IAnalysisScraper analysisScraper,
        IHoldersScraper holdersScraper,
        IFundsScraper fundsScraper,
        INewsScraper newsScraper,
        IEarningsScraper earningsScraper,
        IOptionsScraper optionsScraper,
        IEsgScraper esgScraper,
        ICalendarScraper calendarScraper,
        ISharesScraper sharesScraper,
        IIsinService isinService)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
        _quoteScraper = quoteScraper ?? throw new ArgumentNullException(nameof(quoteScraper));
        _infoScraper = infoScraper ?? throw new ArgumentNullException(nameof(infoScraper));
        _fastInfoScraper = fastInfoScraper ?? throw new ArgumentNullException(nameof(fastInfoScraper));
        _fundamentalsScraper = fundamentalsScraper ?? throw new ArgumentNullException(nameof(fundamentalsScraper));
        _analysisScraper = analysisScraper ?? throw new ArgumentNullException(nameof(analysisScraper));
        _holdersScraper = holdersScraper ?? throw new ArgumentNullException(nameof(holdersScraper));
        _fundsScraper = fundsScraper ?? throw new ArgumentNullException(nameof(fundsScraper));
        _newsScraper = newsScraper ?? throw new ArgumentNullException(nameof(newsScraper));
        _earningsScraper = earningsScraper ?? throw new ArgumentNullException(nameof(earningsScraper));
        _optionsScraper = optionsScraper ?? throw new ArgumentNullException(nameof(optionsScraper));
        _esgScraper = esgScraper ?? throw new ArgumentNullException(nameof(esgScraper));
        _calendarScraper = calendarScraper ?? throw new ArgumentNullException(nameof(calendarScraper));
        _sharesScraper = sharesScraper ?? throw new ArgumentNullException(nameof(sharesScraper));
        _isinService = isinService ?? throw new ArgumentNullException(nameof(isinService));
    }

    /// <summary>
    /// Gets historical price data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="request">The historical data request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Historical price data including OHLC, volume, and adjustments.</returns>
    public Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return _historyScraper.GetHistoryAsync(symbol, request, cancellationToken);
    }

    /// <summary>
    /// Gets metadata about historical data availability for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Metadata about historical data.</returns>
    public Task<HistoryMetadata> GetHistoryMetadataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _historyScraper.GetHistoryMetadataAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets dividend, split, and capital gain actions for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="request">The actions request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Corporate actions data.</returns>
    public async Task<ActionsData> GetActionsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        var historyRequest = new HistoryRequest
        {
            Period = request.Period,
            Start = request.Start,
            End = request.End,
            Interval = Interval.OneDay,
            AutoAdjust = false,
            Repair = false
        };

        var history = await _historyScraper.GetHistoryAsync(symbol, historyRequest, cancellationToken).ConfigureAwait(false);
        return new ActionsData
        {
            Symbol = symbol,
            Dividends = history.Dividends,
            StockSplits = history.StockSplits,
            CapitalGains = history.CapitalGains
        };
    }

    /// <summary>
    /// Gets dividend information for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="request">The actions request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping dividend dates to dividend amounts.</returns>
    public async Task<Dictionary<DateTime, decimal>> GetDividendsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        var actions = await GetActionsAsync(symbol, request, cancellationToken).ConfigureAwait(false);
        return actions.Dividends;
    }

    /// <summary>
    /// Gets stock split information for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="request">The actions request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping split dates to split ratios.</returns>
    public async Task<Dictionary<DateTime, decimal>> GetSplitsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        var actions = await GetActionsAsync(symbol, request, cancellationToken).ConfigureAwait(false);
        return actions.StockSplits;
    }

    /// <summary>
    /// Gets capital gains information for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="request">The actions request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Dictionary mapping capital gains dates to amounts.</returns>
    public async Task<Dictionary<DateTime, decimal>> GetCapitalGainsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        var actions = await GetActionsAsync(symbol, request, cancellationToken).ConfigureAwait(false);
        return actions.CapitalGains;
    }

    /// <summary>
    /// Gets real-time quote data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Current quote data including price, change, and volume.</returns>
    public Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _quoteScraper.GetQuoteAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets detailed information about the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Detailed ticker information.</returns>
    public Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _infoScraper.GetInfoAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets fast info data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Fast info data.</returns>
    public Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fastInfoScraper.GetFastInfoAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets financial statements for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Financial statement data.</returns>
    public Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fundamentalsScraper.GetFinancialStatementsAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets analyst data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Analyst data including ratings and estimates.</returns>
    public Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetAnalystDataAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets institutional and insider holder information for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Holder information.</returns>
    public Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _holdersScraper.GetHoldersAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets fund data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Fund data.</returns>
    public Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fundsScraper.GetFundsDataAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets news items matching the specified request criteria.
    /// </summary>
    /// <param name="request">The news request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of news items.</returns>
    public Task<IReadOnlyList<NewsItem>> GetNewsAsync(NewsRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _newsScraper.GetNewsAsync(request, cancellationToken);
    }

    /// <summary>
    /// Gets earnings estimates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of earnings estimate entries.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetEarningsEstimateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEarningsEstimateAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets revenue estimates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of revenue estimate entries.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetRevenueEstimateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetRevenueEstimateAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets historical earnings data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of earnings history entries.</returns>
    public Task<IReadOnlyList<EarningsHistoryEntry>> GetEarningsHistoryAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEarningsHistoryAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets earnings per share trend data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of EPS trend entries.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetEpsTrendAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEpsTrendAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets earnings per share revisions data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of EPS revisions entries.</returns>
    public Task<IReadOnlyList<PeriodicEstimate>> GetEpsRevisionsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEpsRevisionsAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets growth estimate data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of growth estimate entries.</returns>
    public Task<IReadOnlyList<GrowthEstimateEntry>> GetGrowthEstimatesAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetGrowthEstimatesAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets upcoming earnings dates matching the specified criteria.
    /// </summary>
    /// <param name="request">The earnings dates request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of earnings date entries.</returns>
    public Task<IReadOnlyList<EarningsDateEntry>> GetEarningsDatesAsync(EarningsDatesRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _earningsScraper.GetEarningsDatesAsync(request, cancellationToken);
    }

    /// <summary>
    /// Gets the complete option chain for the specified symbol and expiration.
    /// </summary>
    /// <param name="request">The option chain request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Option chain data including calls and puts.</returns>
    public Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _optionsScraper.GetOptionChainAsync(request, cancellationToken);
    }

    /// <summary>
    /// Gets available option expiration dates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of available expiration dates.</returns>
    public Task<IReadOnlyList<DateTime>> GetOptionsExpirationsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _optionsScraper.GetExpirationsAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets analyst recommendations for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of recommendation trend entries.</returns>
    public Task<IReadOnlyList<RecommendationTrendEntry>> GetRecommendationsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetRecommendationsAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets a summary of analyst recommendations for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recommendations summary data.</returns>
    public Task<RecommendationsSummaryData> GetRecommendationsSummaryAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetRecommendationsSummaryAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets analyst upgrades and downgrades for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of upgrade/downgrade entries.</returns>
    public Task<IReadOnlyList<UpgradeDowngradeEntry>> GetUpgradesDowngradesAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetUpgradesDowngradesAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets ESG (Environmental, Social, and Governance) scores for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ESG data.</returns>
    public Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _esgScraper.GetEsgAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets calendar events (earnings, splits, dividends) for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Calendar data.</returns>
    public Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _calendarScraper.GetCalendarAsync(symbol, cancellationToken);
    }

    /// <summary>
    /// Gets shares outstanding history for the specified symbol.
    /// </summary>
    /// <param name="request">The shares history request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Shares history data.</returns>
    public Task<SharesHistoryData> GetSharesHistoryAsync(SharesHistoryRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _sharesScraper.GetSharesHistoryAsync(request, cancellationToken);
    }

    /// <summary>
    /// Gets the ISIN code for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ISIN code, or null if not found.</returns>
    public Task<string?> GetIsinAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _isinService.GetIsinAsync(symbol, cancellationToken);
    }
}
