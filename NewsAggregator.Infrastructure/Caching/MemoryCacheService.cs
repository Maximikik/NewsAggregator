using Microsoft.Extensions.Caching.Memory;
using NewsAggregator.Application.Common.Caching;

namespace NewsAggregator.Infrastructure.Caching;

public class MemoryCacheService(
    IMemoryCache _cache)
    : ICacheService
{
    private readonly HashSet<string> _keys = [];

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            _cache.Get<T>(key)
            );
    }

    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        _cache.Set(
            key,
            value,
            expiration);

        _keys.Add(key);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellation = default)
    {
        _cache.Remove(key);

        _keys.Remove(key);

        return Task.CompletedTask;
    }

    public Task RemoveByPrefixAsync(string prefix, CancellationToken cancellation = default)
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

        return Task.CompletedTask;
    }
}
