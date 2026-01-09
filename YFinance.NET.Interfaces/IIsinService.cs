namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for resolving ISIN codes.
/// </summary>
public interface IIsinService
{
    /// <summary>
    /// Gets the ISIN code for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ISIN code, or null if not found.</returns>
    Task<string?> GetIsinAsync(string symbol, CancellationToken cancellationToken = default);
}
