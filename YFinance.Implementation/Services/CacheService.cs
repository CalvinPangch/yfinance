using Microsoft.Extensions.Caching.Memory;
using YFinance.Interfaces.Services;
using System.Collections.Concurrent;

namespace YFinance.Implementation.Services;

/// <summary>
/// Memory cache implementation with simple key tracking for clear operations.
/// </summary>
public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ConcurrentDictionary<string, byte> _keys = new();

    public CacheService(IMemoryCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public T? Get<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return default;

        return _cache.TryGetValue(key, out T? value) ? value : default;
    }

    public void Set<T>(string key, T value, int expirationMinutes = 60)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes)
        };

        _cache.Set(key, value, options);
        _keys.TryAdd(key, 0);
    }

    public void Remove(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        _cache.Remove(key);
        _keys.TryRemove(key, out _);
    }

    public void Clear()
    {
        foreach (var key in _keys.Keys)
        {
            _cache.Remove(key);
        }

        _keys.Clear();
    }
}
