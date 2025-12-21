namespace YFinance.Interfaces.Services;

/// <summary>
/// Service interface for managing Yahoo Finance authentication cookies and crumbs.
/// Implements both basic and CSRF cookie acquisition strategies with automatic fallback.
/// </summary>
public interface ICookieService
{
    /// <summary>
    /// Acquires authentication cookies and crumb using available strategies.
    /// Tries basic strategy first, falls back to CSRF if needed.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Tuple of (cookies as string, crumb)</returns>
    Task<(string Cookies, string Crumb)> AcquireCookiesAndCrumbAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates if the current cookies and crumb are still valid.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    bool AreCredentialsValid();

    /// <summary>
    /// Clears cached cookies and crumb, forcing re-acquisition on next request.
    /// </summary>
    void ClearCredentials();
}
