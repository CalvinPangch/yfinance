using System.Net;
using System.Text.RegularExpressions;
using YFinance.NET.Interfaces.Services;
using YFinance.NET.Implementation.Constants;

namespace YFinance.NET.Implementation.Services;

/// <summary>
/// Service for managing Yahoo Finance authentication cookies and crumb tokens.
/// Implements thread-safe singleton pattern for cookie sharing across all requests.
/// </summary>
public class CookieService : ICookieService
{
    private readonly SemaphoreSlim _authLock = new(1, 1);
    private bool _isAuthenticated;
    private string? _crumb;
    private CookieContainer? _cookieContainer;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieService"/> class.
    /// </summary>
    public CookieService()
    {
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

            await AuthenticateAsync(cancellationToken).ConfigureAwait(false);
            return _cookieContainer!;
        }
        finally
        {
            _authLock.Release();
        }
    }

    /// <inheritdoc />
    public string? GetCrumb() => _crumb;

    public async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        await _authLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            _isAuthenticated = false;
            _crumb = null;
            _cookieContainer = null;
            await AuthenticateAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _authLock.Release();
        }
    }

    private async Task AuthenticateAsync(CancellationToken cancellationToken)
    {
        _cookieContainer = new CookieContainer();
        var handler = new HttpClientHandler
        {
            CookieContainer = _cookieContainer,
            UseCookies = true
        };

        using var tempClient = new HttpClient(handler);
        tempClient.DefaultRequestHeaders.Add("User-Agent", YahooFinanceConstants.Headers.UserAgent);

        // Attempt consent page to trigger cookies
        await tempClient.GetAsync(YahooFinanceConstants.BaseUrls.Consent, cancellationToken)
            .ConfigureAwait(false);

        // Try to get crumb (optional)
        _crumb = await TryGetCrumbAsync(tempClient, cancellationToken).ConfigureAwait(false);

        // If crumb is missing, retry against query2 with consent cookies
        if (string.IsNullOrEmpty(_crumb))
        {
            await tempClient.GetAsync(YahooFinanceConstants.BaseUrls.Query2, cancellationToken)
                .ConfigureAwait(false);
            _crumb = await TryGetCrumbAsync(tempClient, cancellationToken).ConfigureAwait(false);
        }

        // CSRF consent flow fallback
        if (string.IsNullOrEmpty(_crumb))
        {
            await TryExecuteCsrfConsentAsync(tempClient, cancellationToken).ConfigureAwait(false);
            _crumb = await TryGetCrumbAsync(tempClient, cancellationToken).ConfigureAwait(false);
        }

        _isAuthenticated = true;
    }

    private static async Task<string?> TryGetCrumbAsync(HttpClient client, CancellationToken cancellationToken)
    {
        try
        {
            var crumbResponse = await client.GetAsync(
                YahooFinanceConstants.BaseUrls.Query1 + YahooFinanceConstants.Endpoints.Crumb,
                cancellationToken).ConfigureAwait(false);

            if (!crumbResponse.IsSuccessStatusCode)
                return null;

            return await crumbResponse.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return null;
        }
    }

    private static async Task TryExecuteCsrfConsentAsync(HttpClient client, CancellationToken cancellationToken)
    {
        try
        {
            var landing = await client.GetAsync(YahooFinanceConstants.BaseUrls.Query2, cancellationToken)
                .ConfigureAwait(false);
            var html = await landing.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var csrfToken = ExtractInputValue(html, "csrfToken");
            var sessionId = ExtractInputValue(html, "sessionId");

            if (string.IsNullOrEmpty(csrfToken) || string.IsNullOrEmpty(sessionId))
                return;

            var form = new Dictionary<string, string>
            {
                { "csrfToken", csrfToken },
                { "sessionId", sessionId },
                { "originalDoneUrl", YahooFinanceConstants.BaseUrls.Query2 },
                { "namespace", "yahoo" },
                { "agree", "agree" }
            };

            using var content = new FormUrlEncodedContent(form);
            var submitUrl = $"{YahooFinanceConstants.BaseUrls.ConsentV2}/v2/collectConsent";
            await client.PostAsync(submitUrl, content, cancellationToken).ConfigureAwait(false);

            var confirmUrl = $"{YahooFinanceConstants.BaseUrls.ConsentV2}/v2/consent?sessionId={Uri.EscapeDataString(sessionId)}";
            await client.GetAsync(confirmUrl, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException)
        {
            // Ignore and continue without CSRF consent.
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            // Ignore timeout.
        }
    }

    private static string? ExtractInputValue(string html, string inputName)
    {
        if (string.IsNullOrEmpty(html))
            return null;

        var pattern = $@"name=""{Regex.Escape(inputName)}""\s+value=""([^""]+)""";
        var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value : null;
    }

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
