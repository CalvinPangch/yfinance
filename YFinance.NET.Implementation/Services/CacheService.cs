using Microsoft.Extensions.Caching.Memory;
using YFinance.NET.Interfaces.Services;
using System.Collections.Concurrent;

namespace YFinance.NET.Implementation.Services;

/// <summary>
/// Memory cache implementation with simple key tracking for clear operations.
/// </summary>
public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ConcurrentDictionary<string, byte> _keys = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheService"/> class.
    /// </summary>
    /// <param name="cache">The underlying memory cache.</param>
    public CacheService(IMemoryCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    /// <summary>
    /// Gets a cached value by key.
    /// </summary>
    /// <typeparam name="T">The type of the cached value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <returns>The cached value, or default if not found.</returns>
    public T? Get<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return default;

        return _cache.TryGetValue(key, out T? value) ? value : default;
    }

    /// <summary>
    /// Sets a cached value with optional expiration.
    /// </summary>
    /// <typeparam name="T">The type of the value to cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The value to cache.</param>
    /// <param name="expirationMinutes">Expiration time in minutes (default 60).</param>
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

    /// <summary>
    /// Removes a cached value by key.
    /// </summary>
    /// <param name="key">The cache key.</param>
    public void Remove(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        _cache.Remove(key);
        _keys.TryRemove(key, out _);
    }

    /// <summary>
    /// Clears all cached values.
    /// </summary>
    public void Clear()
    {
        foreach (var key in _keys.Keys)
        {
            _cache.Remove(key);
        }

        _keys.Clear();
    }
}
