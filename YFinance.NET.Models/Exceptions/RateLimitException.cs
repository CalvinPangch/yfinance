namespace YFinance.NET.Models.Exceptions;

/// <summary>
/// Exception thrown when Yahoo Finance API rate limit is exceeded.
/// </summary>
public class RateLimitException : YahooFinanceException
{
    public int RetryAfterSeconds { get; }

    public RateLimitException() : base("Yahoo Finance API rate limit exceeded.")
    {
        RetryAfterSeconds = 60;
    }

    public RateLimitException(int retryAfterSeconds)
        : base($"Yahoo Finance API rate limit exceeded. Retry after {retryAfterSeconds} seconds.")
    {
        RetryAfterSeconds = retryAfterSeconds;
    }

    public RateLimitException(string message) : base(message)
    {
        RetryAfterSeconds = 60;
    }
}
