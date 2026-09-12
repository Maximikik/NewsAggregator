using Microsoft.Extensions.Caching.Memory;
using NewsAggregator.Application.Common.Caching;
using System.Collections.Concurrent;

namespace NewsAggregator.Infrastructure.Caching;

public class MemoryCacheService(
    IMemoryCache _cache)
    : ICacheService
{
    private readonly ConcurrentDictionary<string, byte> _keys = [];

    public T? Get<T>(string key) => _cache.Get<T>(key);

    public void Set<T>(
        string key,
        T value,
        TimeSpan expiration)
    {
        _cache.Set(
            key,
            value,
            expiration);

        _keys.TryAdd(key, 0);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);

        _keys.TryRemove(key, out _);
    }

    public void RemoveByPrefix(string prefix)
    {
        var keys = _keys.Keys
            .Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var key in keys)
        {
            _cache.Remove(key);
            _keys.TryRemove(key, out _);
        }
    }
}
