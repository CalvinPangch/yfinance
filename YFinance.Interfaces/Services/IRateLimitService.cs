namespace YFinance.Interfaces.Services;

/// <summary>
/// Service interface for detecting and handling rate limiting.
/// </summary>
public interface IRateLimitService
{
    /// <summary>
    /// Checks if a response indicates rate limiting.
    /// </summary>
    /// <param name="statusCode">HTTP status code</param>
    /// <param name="responseBody">Response body</param>
    /// <returns>True if rate limited, false otherwise</returns>
    bool IsRateLimited(int statusCode, string responseBody);

    /// <summary>
    /// Handles rate limit errors with exponential backoff.
    /// </summary>
    /// <param name="retryCount">Current retry attempt</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task HandleRateLimitAsync(int retryCount, CancellationToken cancellationToken = default);
}
