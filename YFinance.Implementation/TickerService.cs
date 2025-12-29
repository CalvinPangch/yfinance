using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Models;
using YFinance.Models.Requests;
using YFinance.Models.Enums;

namespace YFinance.Implementation;

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

    public Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return _historyScraper.GetHistoryAsync(symbol, request, cancellationToken);
    }

    public Task<HistoryMetadata> GetHistoryMetadataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _historyScraper.GetHistoryMetadataAsync(symbol, cancellationToken);
    }

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

    public async Task<Dictionary<DateTime, decimal>> GetDividendsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        var actions = await GetActionsAsync(symbol, request, cancellationToken).ConfigureAwait(false);
        return actions.Dividends;
    }

    public async Task<Dictionary<DateTime, decimal>> GetSplitsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        var actions = await GetActionsAsync(symbol, request, cancellationToken).ConfigureAwait(false);
        return actions.StockSplits;
    }

    public async Task<Dictionary<DateTime, decimal>> GetCapitalGainsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default)
    {
        var actions = await GetActionsAsync(symbol, request, cancellationToken).ConfigureAwait(false);
        return actions.CapitalGains;
    }

    public Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _quoteScraper.GetQuoteAsync(symbol, cancellationToken);
    }

    public Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _infoScraper.GetInfoAsync(symbol, cancellationToken);
    }

    public Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fastInfoScraper.GetFastInfoAsync(symbol, cancellationToken);
    }

    public Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fundamentalsScraper.GetFinancialStatementsAsync(symbol, cancellationToken);
    }

    public Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetAnalystDataAsync(symbol, cancellationToken);
    }

    public Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _holdersScraper.GetHoldersAsync(symbol, cancellationToken);
    }

    public Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _fundsScraper.GetFundsDataAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<NewsItem>> GetNewsAsync(NewsRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _newsScraper.GetNewsAsync(request, cancellationToken);
    }

    public Task<IReadOnlyList<PeriodicEstimate>> GetEarningsEstimateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEarningsEstimateAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<PeriodicEstimate>> GetRevenueEstimateAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetRevenueEstimateAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<EarningsHistoryEntry>> GetEarningsHistoryAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEarningsHistoryAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<PeriodicEstimate>> GetEpsTrendAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEpsTrendAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<PeriodicEstimate>> GetEpsRevisionsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetEpsRevisionsAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<GrowthEstimateEntry>> GetGrowthEstimatesAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _earningsScraper.GetGrowthEstimatesAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<EarningsDateEntry>> GetEarningsDatesAsync(EarningsDatesRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _earningsScraper.GetEarningsDatesAsync(request, cancellationToken);
    }

    public Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _optionsScraper.GetOptionChainAsync(request, cancellationToken);
    }

    public Task<IReadOnlyList<DateTime>> GetOptionsExpirationsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _optionsScraper.GetExpirationsAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<RecommendationTrendEntry>> GetRecommendationsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetRecommendationsAsync(symbol, cancellationToken);
    }

    public Task<IReadOnlyList<UpgradeDowngradeEntry>> GetUpgradesDowngradesAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _analysisScraper.GetUpgradesDowngradesAsync(symbol, cancellationToken);
    }

    public Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _esgScraper.GetEsgAsync(symbol, cancellationToken);
    }

    public Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _calendarScraper.GetCalendarAsync(symbol, cancellationToken);
    }

    public Task<SharesHistoryData> GetSharesHistoryAsync(SharesHistoryRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _sharesScraper.GetSharesHistoryAsync(request, cancellationToken);
    }

    public Task<string?> GetIsinAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        return _isinService.GetIsinAsync(symbol, cancellationToken);
    }
}
