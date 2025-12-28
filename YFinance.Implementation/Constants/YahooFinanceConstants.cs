namespace YFinance.Implementation.Constants;

/// <summary>
/// Constants for Yahoo Finance API URLs, endpoints, and default values.
/// </summary>
internal static class YahooFinanceConstants
{
    /// <summary>
    /// Base URLs for Yahoo Finance services.
    /// </summary>
    public static class BaseUrls
    {
        /// <summary>
        /// Primary query endpoint (query1.finance.yahoo.com).
        /// </summary>
        public const string Query1 = "https://query1.finance.yahoo.com";

        /// <summary>
        /// Secondary query endpoint (query2.finance.yahoo.com).
        /// </summary>
        public const string Query2 = "https://query2.finance.yahoo.com";

        /// <summary>
        /// Consent page URL for cookie acquisition (fc.yahoo.com).
        /// </summary>
        public const string Consent = "https://fc.yahoo.com";

        /// <summary>
        /// Consent service base URL.
        /// </summary>
        public const string ConsentV2 = "https://consent.yahoo.com";

        /// <summary>
        /// Yahoo root domain.
        /// </summary>
        public const string Yahoo = "https://yahoo.com";
    }

    /// <summary>
    /// API endpoint paths.
    /// </summary>
    public static class Endpoints
    {
        /// <summary>
        /// Historical chart data endpoint.
        /// Format: /v8/finance/chart/{symbol}
        /// </summary>
        public const string Chart = "/v8/finance/chart/{0}";

        /// <summary>
        /// Quote summary endpoint.
        /// Format: /v10/finance/quoteSummary/{symbol}
        /// </summary>
        public const string QuoteSummary = "/v10/finance/quoteSummary/{0}";

        /// <summary>
        /// Crumb acquisition endpoint.
        /// </summary>
        public const string Crumb = "/v1/test/getcrumb";

        /// <summary>
        /// Financial data endpoint.
        /// </summary>
        public const string Financials = "/v10/finance/quoteSummary/{0}?modules=financialData";

        /// <summary>
        /// Analyst recommendations endpoint.
        /// </summary>
        public const string Analysis = "/v10/finance/quoteSummary/{0}?modules=recommendationTrend";

        /// <summary>
        /// Holders information endpoint.
        /// </summary>
        public const string Holders = "/v10/finance/quoteSummary/{0}?modules=majorHoldersBreakdown,institutionOwnership";
    }

    /// <summary>
    /// HTTP headers and user agent strings.
    /// </summary>
    public static class Headers
    {
        /// <summary>
        /// User-Agent header value for API requests.
        /// </summary>
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
    }

    /// <summary>
    /// Query parameter keys and default values.
    /// </summary>
    public static class QueryParams
    {
        /// <summary>
        /// Events to include (dividends and splits).
        /// </summary>
        public const string Events = "div,split";

        /// <summary>
        /// Include pre-market and post-market data.
        /// </summary>
        public const string IncludePrePost = "false";

        /// <summary>
        /// Data interval parameter key.
        /// </summary>
        public const string Interval = "interval";

        /// <summary>
        /// Time range parameter key.
        /// </summary>
        public const string Range = "range";

        /// <summary>
        /// Start date parameter key (Unix timestamp).
        /// </summary>
        public const string Period1 = "period1";

        /// <summary>
        /// End date parameter key (Unix timestamp).
        /// </summary>
        public const string Period2 = "period2";
    }

    /// <summary>
    /// Default timeout values (in milliseconds).
    /// </summary>
    public static class Timeouts
    {
        /// <summary>
        /// Default HTTP request timeout (30 seconds).
        /// </summary>
        public const int DefaultRequest = 30000;

        /// <summary>
        /// Authentication timeout (10 seconds).
        /// </summary>
        public const int Authentication = 10000;
    }
}
