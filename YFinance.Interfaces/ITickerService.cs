using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Interfaces;

/// <summary>
/// Main service interface for ticker operations.
/// Provides access to all ticker-related data including quotes, history, financials, and analysis.
/// </summary>
public interface ITickerService
{
    /// <summary>
    /// Gets quote data for a specific ticker symbol.
    /// </summary>
    /// <param name="symbol">Ticker symbol (e.g., "AAPL")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Quote data including current price, market cap, P/E ratio, etc.</returns>
    Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets raw info payload for a ticker.
    /// </summary>
    Task<InfoData> GetInfoAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets fast info snapshot derived from history and metadata.
    /// </summary>
    Task<FastInfoData> GetFastInfoAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets historical price data for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="request">History request parameters (period, interval, start/end dates)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Historical OHLC data with dividends and splits</returns>
    Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets history metadata (timezone, trading periods) for a ticker.
    /// </summary>
    Task<HistoryMetadata> GetHistoryMetadataAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets corporate actions (dividends, splits, capital gains).
    /// </summary>
    Task<ActionsData> GetActionsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets dividends for a ticker.
    /// </summary>
    Task<Dictionary<DateTime, decimal>> GetDividendsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets stock splits for a ticker.
    /// </summary>
    Task<Dictionary<DateTime, decimal>> GetSplitsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets capital gains for a ticker.
    /// </summary>
    Task<Dictionary<DateTime, decimal>> GetCapitalGainsAsync(string symbol, ActionsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets financial statement data (income statement, balance sheet, cash flow).
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Financial statement data</returns>
    Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets analyst recommendations and price targets.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Analyst data including recommendations and price targets</returns>
    Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets institutional and insider holdings data.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Holder data including major holders and institutional investors</returns>
    Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets ETF or mutual fund data.
    /// </summary>
    /// <param name="symbol">Fund symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Funds data including holdings and profile</returns>
    Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets news items for a ticker.
    /// </summary>
    /// <param name="request">News request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>News items</returns>
    Task<IReadOnlyList<NewsItem>> GetNewsAsync(NewsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets earnings estimates from earnings trend data.
    /// </summary>
    Task<IReadOnlyList<PeriodicEstimate>> GetEarningsEstimateAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets revenue estimates from earnings trend data.
    /// </summary>
    Task<IReadOnlyList<PeriodicEstimate>> GetRevenueEstimateAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets earnings history data.
    /// </summary>
    Task<IReadOnlyList<EarningsHistoryEntry>> GetEarningsHistoryAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets EPS trend data.
    /// </summary>
    Task<IReadOnlyList<PeriodicEstimate>> GetEpsTrendAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets EPS revisions data.
    /// </summary>
    Task<IReadOnlyList<PeriodicEstimate>> GetEpsRevisionsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets growth estimates for stock, industry, sector, and index trends.
    /// </summary>
    Task<IReadOnlyList<GrowthEstimateEntry>> GetGrowthEstimatesAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets earnings dates.
    /// </summary>
    Task<IReadOnlyList<EarningsDateEntry>> GetEarningsDatesAsync(EarningsDatesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets option chain data for a ticker.
    /// </summary>
    Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets available option expiration dates.
    /// </summary>
    Task<IReadOnlyList<DateTime>> GetOptionsExpirationsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets recommendation trend history.
    /// </summary>
    Task<IReadOnlyList<RecommendationTrendEntry>> GetRecommendationsAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets analyst upgrades/downgrades history.
    /// </summary>
    Task<IReadOnlyList<UpgradeDowngradeEntry>> GetUpgradesDowngradesAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets ESG score data.
    /// </summary>
    Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets calendar events (earnings, dividends, capital gains).
    /// </summary>
    Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets shares outstanding and float history.
    /// </summary>
    Task<SharesHistoryData> GetSharesHistoryAsync(SharesHistoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resolves ISIN for a ticker symbol.
    /// </summary>
    Task<string?> GetIsinAsync(string symbol, CancellationToken cancellationToken = default);
}
