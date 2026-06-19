using Microsoft.Extensions.Caching.Memory;
using NewsAggregator.Application.Common.Caching;

namespace NewsAggregator.Infrastructure.Caching;

public class MemoryCacheService(
    IMemoryCache _cache)
    : ICacheService
{
    private readonly HashSet<string> _keys = [];

    public T? Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public void Set<T>(
        string key,
        T value,
        TimeSpan expiration)
    {
        _cache.Set(
            key,
            value,
            expiration);

        _keys.Add(key);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);

        _keys.Remove(key);
    }

    public void RemoveByPrefix(string prefix)
    {
        var keys =
            _keys
                .Where(
                    x => x.StartsWith(
                        prefix,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

        foreach (var key in keys)
        {
            _cache.Remove(key);

            _keys.Remove(key);
        }
    }
}
