using NewsAggregator.Application.Common.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace NewsAggregator.Infrastructure.Caching;

public sealed class RedisCacheService(
    IConnectionMultiplexer _redis)
    : ICacheService
{
    private IDatabase Database => _redis.GetDatabase();
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await Database.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>((string)value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        var serialized = JsonSerializer.Serialize(value);

        await Database.StringSetAsync(key, serialized, expiration);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellation = default)
    {
        await Database.KeyDeleteAsync(key);
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var endpoint = _redis.GetEndPoints().First();
        var server = _redis.GetServer(endpoint);

        var keys = server
            .Keys(database: 0, pattern: $"{prefix}")
            .ToArray();

        if (keys.Length > 0)
        {
            await Database.KeyDeleteAsync(keys);
        }
    }
}
