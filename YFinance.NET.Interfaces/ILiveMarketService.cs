using YFinance.NET.Models;

namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for real-time streaming quotes via Yahoo Finance WebSocket.
/// </summary>
public interface ILiveMarketService
{
    Task ListenAsync(IEnumerable<string> symbols, Func<LivePriceData, Task>? onMessage = null, CancellationToken cancellationToken = default);
}
