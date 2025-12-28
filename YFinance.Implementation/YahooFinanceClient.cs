using System.Net;
using YFinance.Interfaces;
using YFinance.Interfaces.Services;
using YFinance.Implementation.Constants;
using YFinance.Models.Exceptions;

namespace YFinance.Implementation;

/// <summary>
/// HTTP client for Yahoo Finance API interactions.
/// Stateless service that delegates authentication to ICookieService.
/// Includes retry logic with exponential backoff and rate limit handling.
/// </summary>
public class YahooFinanceClient : IYahooFinanceClient
{
    private readonly ICookieService _cookieService;
    private readonly IRateLimitService _rateLimitService;
    private readonly ICacheService? _cacheService;
    private const int MaxRetries = 3;

    /// <summary>
    /// Initializes a new instance of the <see cref="YahooFinanceClient"/> class.
    /// </summary>
    /// <param name="cookieService">The cookie service for authentication.</param>
    /// <param name="rateLimitService">The rate limit service for handling API limits.</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
    public YahooFinanceClient(
        ICookieService cookieService,
        IRateLimitService rateLimitService,
        ICacheService? cacheService = null)
    {
        _cookieService = cookieService ?? throw new ArgumentNullException(nameof(cookieService));
        _rateLimitService = rateLimitService ?? throw new ArgumentNullException(nameof(rateLimitService));
        _cacheService = cacheService;
    }

    /// <inheritdoc />
    public async Task<string> GetAsync(string endpoint, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));

        // Extract symbol from endpoint for better error messages (e.g., /v8/finance/chart/AAPL -> AAPL)
        var symbol = ExtractSymbolFromEndpoint(endpoint);
        var cacheKey = BuildCacheKey(endpoint, queryParams);

        if (_cacheService != null)
        {
            var cached = _cacheService.Get<string>(cacheKey);
            if (!string.IsNullOrEmpty(cached))
                return cached;
        }

        var responseBody = await ExecuteWithRetryAsync(
            async () =>
            {
                var cookieContainer = await _cookieService.GetCookieContainerAsync(cancellationToken)
                    .ConfigureAwait(false);

                var handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri(YahooFinanceConstants.BaseUrls.Query1);
                client.DefaultRequestHeaders.Add("User-Agent", YahooFinanceConstants.Headers.UserAgent);

                // Inject crumb into query parameters
                var enrichedParams = InjectCrumb(queryParams);

                var url = BuildUrl(endpoint, enrichedParams);
                return await client.GetAsync(url, cancellationToken).ConfigureAwait(false);
            },
            symbol,
            cancellationToken)
            .ConfigureAwait(false);

        _cacheService?.Set(cacheKey, responseBody, 10);
        return responseBody;
    }

    private static string BuildUrl(string endpoint, Dictionary<string, string>? queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return endpoint;

        var queryString = string.Join("&",
            queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
        return $"{endpoint}?{queryString}";
    }

    private static string BuildCacheKey(string endpoint, Dictionary<string, string>? queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return endpoint;

        var normalized = string.Join("&",
            queryParams.OrderBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key}={kvp.Value}"));
        return $"{endpoint}?{normalized}";
    }

    private Dictionary<string, string>? InjectCrumb(Dictionary<string, string>? queryParams)
    {
        var crumb = _cookieService.GetCrumb();

        if (string.IsNullOrEmpty(crumb))
            return queryParams;

        var enrichedParams = queryParams != null
            ? new Dictionary<string, string>(queryParams)
            : new Dictionary<string, string>();

        enrichedParams["crumb"] = crumb;
        return enrichedParams;
    }

    private static string ExtractSymbolFromEndpoint(string endpoint)
    {
        // Extract symbol from common patterns like /v8/finance/chart/AAPL or /v10/finance/quoteSummary/MSFT
        var parts = endpoint.Split('/', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 0 ? parts[^1] : "UNKNOWN";
    }

    private async Task<string> ExecuteWithRetryAsync(
        Func<Task<HttpResponseMessage>> httpCall,
        string symbol,
        CancellationToken cancellationToken)
    {
        int retryCount = 0;

        while (true)
        {
            try
            {
                var response = await httpCall().ConfigureAwait(false);
                var statusCode = (int)response.StatusCode;

                // Handle 404 - Invalid Ticker
                if (statusCode == 404)
                {
                    throw new InvalidTickerException(symbol);
                }

                // Read content before checking for rate limiting
                var content = await response.Content.ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);

                // Check for rate limiting
                if (_rateLimitService.IsRateLimited(statusCode, content))
                {
                    await _rateLimitService.HandleRateLimitAsync(retryCount++, cancellationToken)
                        .ConfigureAwait(false);
                    continue; // Retry the request
                }

                if (statusCode == 401 || statusCode == 403 || content.Contains("crumb", StringComparison.OrdinalIgnoreCase))
                {
                    if (retryCount >= MaxRetries)
                        response.EnsureSuccessStatusCode();

                    await _cookieService.RefreshAsync(cancellationToken).ConfigureAwait(false);
                    retryCount++;
                    continue;
                }

                // Handle 5xx server errors with retry
                if (statusCode >= 500 && statusCode < 600)
                {
                    if (retryCount >= MaxRetries)
                    {
                        response.EnsureSuccessStatusCode(); // This will throw
                    }

                    // Exponential backoff for server errors
                    var delayMs = 1000 * (int)Math.Pow(2, retryCount);
                    await Task.Delay(delayMs, cancellationToken).ConfigureAwait(false);
                    retryCount++;
                    continue; // Retry the request
                }

                // For all other status codes, ensure success or throw
                response.EnsureSuccessStatusCode();

                return content;
            }
            catch (HttpRequestException) when (retryCount < MaxRetries)
            {
                // Handle transient network errors with exponential backoff
                var delayMs = 1000 * (int)Math.Pow(2, retryCount);
                await Task.Delay(delayMs, cancellationToken).ConfigureAwait(false);
                retryCount++;
                // Continue to retry
            }
            catch (HttpRequestException)
            {
                // Max retries exceeded, rethrow
                throw;
            }
        }
    }

    /// <inheritdoc />
    public async Task<string> PostAsync(string endpoint, string jsonBody, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));

        if (jsonBody == null)
            throw new ArgumentNullException(nameof(jsonBody));

        var cookieContainer = await _cookieService.GetCookieContainerAsync(cancellationToken)
            .ConfigureAwait(false);

        var handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer,
            UseCookies = true
        };

        using var client = new HttpClient(handler);
        client.BaseAddress = new Uri(YahooFinanceConstants.BaseUrls.Query1);
        client.DefaultRequestHeaders.Add("User-Agent", YahooFinanceConstants.Headers.UserAgent);

        var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(endpoint, content, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task EnsureAuthenticationAsync(CancellationToken cancellationToken = default)
    {
        // Authentication is now handled by ICookieService
        // This method is kept for interface compatibility but delegates to the service
        return _cookieService.GetCookieContainerAsync(cancellationToken);
    }
}
