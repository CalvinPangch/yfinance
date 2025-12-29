using YFinance.NET.Interfaces.Services;
using YFinance.NET.Models.Exceptions;

namespace YFinance.NET.Implementation.Services;

/// <summary>
/// Service for detecting and handling rate limiting from Yahoo Finance API.
/// Implements exponential backoff strategy for retry attempts.
/// </summary>
public class RateLimitService : IRateLimitService
{
    private const int BaseDelayMs = 1000;
    private const int MaxRetries = 5;

    /// <inheritdoc />
    public bool IsRateLimited(int statusCode, string responseBody)
    {
        if (statusCode == 429)
            return true;

        if (string.IsNullOrEmpty(responseBody))
            return false;

        return responseBody.Contains("Too Many Requests", StringComparison.OrdinalIgnoreCase) ||
               responseBody.Contains("rate limit", StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public async Task HandleRateLimitAsync(int retryCount, CancellationToken cancellationToken = default)
    {
        if (retryCount >= MaxRetries)
        {
            throw new RateLimitException(60); // Suggest 60 second retry after
        }

        // Exponential backoff: 1s, 2s, 4s, 8s, 16s
        var delayMs = BaseDelayMs * (int)Math.Pow(2, retryCount);
        await Task.Delay(delayMs, cancellationToken).ConfigureAwait(false);
    }
}
