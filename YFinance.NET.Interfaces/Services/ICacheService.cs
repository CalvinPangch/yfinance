namespace YFinance.NET.Interfaces.Services;

/// <summary>
/// Service interface for caching HTTP responses and data.
/// Implements LRU caching with configurable expiration.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets a cached value by key.
    /// </summary>
    /// <typeparam name="T">Type of cached value</typeparam>
    /// <param name="key">Cache key</param>
    /// <returns>Cached value or default if not found</returns>
    T? Get<T>(string key);

    /// <summary>
    /// Sets a value in the cache with optional expiration.
    /// </summary>
    /// <typeparam name="T">Type of value to cache</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">Value to cache</param>
    /// <param name="expirationMinutes">Optional expiration in minutes (default: 60)</param>
    void Set<T>(string key, T value, int expirationMinutes = 60);

    /// <summary>
    /// Removes a specific key from the cache.
    /// </summary>
    /// <param name="key">Cache key to remove</param>
    void Remove(string key);

    /// <summary>
    /// Clears all cached data.
    /// </summary>
    void Clear();
}
