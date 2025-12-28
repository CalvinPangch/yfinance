using System.Net;

namespace YFinance.Interfaces.Services;

/// <summary>
/// Service for managing Yahoo Finance authentication cookies and crumb tokens.
/// Implements thread-safe singleton pattern for cookie sharing across all requests.
/// </summary>
/// <remarks>
/// This service should be registered as Singleton in the DI container.
/// It maintains a shared CookieContainer and crumb token that persist
/// across all HTTP requests to Yahoo Finance, reducing authentication overhead.
/// </remarks>
public interface ICookieService : IDisposable
{
    /// <summary>
    /// Gets the shared cookie container, acquiring authentication if needed.
    /// Thread-safe and idempotent - multiple concurrent calls will only authenticate once.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to abort authentication.</param>
    /// <returns>A cookie container with Yahoo Finance authentication cookies.</returns>
    /// <exception cref="HttpRequestException">Thrown when authentication request fails.</exception>
    /// <exception cref="OperationCanceledException">Thrown when operation is cancelled.</exception>
    Task<CookieContainer> GetCookieContainerAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the crumb token acquired during authentication.
    /// Returns null if authentication hasn't been performed yet or if crumb is unavailable.
    /// </summary>
    /// <returns>The crumb token, or null if not available.</returns>
    string? GetCrumb();
}
