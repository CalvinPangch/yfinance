using System.Net;
using YFinance.Interfaces;

namespace YFinance.Implementation;

/// <summary>
/// HTTP client for Yahoo Finance API interactions.
/// Handles authentication, cookie management, and HTTP requests.
/// </summary>
public class YahooFinanceClient : IYahooFinanceClient
{
    private readonly HttpClient _httpClient;
    private string? _crumb;
    private bool _isAuthenticated;
    private readonly SemaphoreSlim _authLock = new(1, 1);

    public YahooFinanceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://query1.finance.yahoo.com");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
    }

    public async Task<string> GetAsync(string endpoint, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
    {
        await EnsureAuthenticationAsync(cancellationToken);

        var url = endpoint;
        if (queryParams != null && queryParams.Count > 0)
        {
            var queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            url = $"{endpoint}?{queryString}";
        }

        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> PostAsync(string endpoint, string jsonBody, CancellationToken cancellationToken = default)
    {
        await EnsureAuthenticationAsync(cancellationToken);

        var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task EnsureAuthenticationAsync(CancellationToken cancellationToken = default)
    {
        if (_isAuthenticated)
            return;

        await _authLock.WaitAsync(cancellationToken);
        try
        {
            if (_isAuthenticated)
                return;

            // Basic cookie acquisition - fetch from consent page
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            };

            using var tempClient = new HttpClient(handler);
            tempClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

            // Get initial cookies from Yahoo Finance
            var response = await tempClient.GetAsync("https://fc.yahoo.com", cancellationToken);

            // Try to get crumb
            try
            {
                var crumbResponse = await tempClient.GetAsync("https://query1.finance.yahoo.com/v1/test/getcrumb", cancellationToken);
                if (crumbResponse.IsSuccessStatusCode)
                {
                    _crumb = await crumbResponse.Content.ReadAsStringAsync(cancellationToken);
                }
            }
            catch
            {
                // Crumb is optional for many endpoints
            }

            // Copy cookies to main client
            var cookies = cookieContainer.GetCookies(new Uri("https://yahoo.com"));
            foreach (Cookie cookie in cookies)
            {
                _httpClient.DefaultRequestHeaders.Add("Cookie", $"{cookie.Name}={cookie.Value}");
            }

            _isAuthenticated = true;
        }
        finally
        {
            _authLock.Release();
        }
    }
}
