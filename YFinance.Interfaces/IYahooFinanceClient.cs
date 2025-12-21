namespace YFinance.Interfaces;

/// <summary>
/// HTTP client interface for Yahoo Finance API interactions.
/// Handles cookie management, authentication, and HTTP requests.
/// </summary>
public interface IYahooFinanceClient
{
    /// <summary>
    /// Sends a GET request to the specified endpoint with optional query parameters.
    /// </summary>
    /// <param name="endpoint">The API endpoint (e.g., "/v8/finance/chart/{ticker}")</param>
    /// <param name="queryParams">Optional query parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>JSON response as string</returns>
    Task<string> GetAsync(string endpoint, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request to the specified endpoint with JSON body.
    /// </summary>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="jsonBody">JSON request body</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>JSON response as string</returns>
    Task<string> PostAsync(string endpoint, string jsonBody, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures cookies and crumb are valid, refreshing if necessary.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task EnsureAuthenticationAsync(CancellationToken cancellationToken = default);
}
