using YFinance.NET.Models;

namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for real-time streaming quotes via Yahoo Finance WebSocket.
/// </summary>
public interface ILiveMarketService
{
    /// <summary>
    /// Listens for real-time price updates for the specified symbols.
    /// </summary>
    /// <param name="symbols">The ticker symbols to listen for.</param>
    /// <param name="onMessage">Optional callback to handle price updates.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that completes when the listening is finished.</returns>
    Task ListenAsync(IEnumerable<string> symbols, Func<LivePriceData, Task>? onMessage = null, CancellationToken cancellationToken = default);
}
