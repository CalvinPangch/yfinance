using System.Net;
using Microsoft.Extensions.Http;
using YFinance.Interfaces.Services;
using YFinance.Implementation.Constants;

namespace YFinance.Implementation.Services;

/// <summary>
/// Service for managing Yahoo Finance authentication cookies and crumb tokens.
/// Implements thread-safe singleton pattern for cookie sharing across all requests.
/// </summary>
public class CookieService : ICookieService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SemaphoreSlim _authLock = new(1, 1);
    private bool _isAuthenticated;
    private string? _crumb;
    private CookieContainer? _cookieContainer;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory for creating HTTP clients.</param>
    /// <exception cref="ArgumentNullException">Thrown when httpClientFactory is null.</exception>
    public CookieService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <inheritdoc />
    public async Task<CookieContainer> GetCookieContainerAsync(CancellationToken cancellationToken = default)
    {
        if (_isAuthenticated && _cookieContainer != null)
            return _cookieContainer;

        await _authLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            // Double-check after acquiring lock
            if (_isAuthenticated && _cookieContainer != null)
                return _cookieContainer;

            _cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true
            };

            using var tempClient = new HttpClient(handler);
            tempClient.DefaultRequestHeaders.Add("User-Agent", YahooFinanceConstants.Headers.UserAgent);

            // Get initial cookies from Yahoo Finance consent page
            await tempClient.GetAsync(YahooFinanceConstants.BaseUrls.Consent, cancellationToken)
                .ConfigureAwait(false);

            // Try to get crumb (optional - some endpoints work without it)
            try
            {
                var crumbResponse = await tempClient.GetAsync(
                    YahooFinanceConstants.BaseUrls.Query1 + YahooFinanceConstants.Endpoints.Crumb,
                    cancellationToken).ConfigureAwait(false);

                if (crumbResponse.IsSuccessStatusCode)
                {
                    _crumb = await crumbResponse.Content.ReadAsStringAsync(cancellationToken)
                        .ConfigureAwait(false);
                }
            }
            catch (HttpRequestException)
            {
                // Crumb is optional for many endpoints
                // Continue without it
            }
            catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                // Timeout occurred but not user-requested cancellation
                // Continue without crumb
            }

            _isAuthenticated = true;
            return _cookieContainer;
        }
        finally
        {
            _authLock.Release();
        }
    }

    /// <inheritdoc />
    public string? GetCrumb() => _crumb;

    /// <summary>
    /// Releases all resources used by the <see cref="CookieService"/>.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _authLock?.Dispose();
            }
            _disposed = true;
        }
    }
}
