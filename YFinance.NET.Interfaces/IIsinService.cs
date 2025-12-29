namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for resolving ISIN codes.
/// </summary>
public interface IIsinService
{
    Task<string?> GetIsinAsync(string symbol, CancellationToken cancellationToken = default);
}
