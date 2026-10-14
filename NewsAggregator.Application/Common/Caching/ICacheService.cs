namespace NewsAggregator.Application.Common.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(string key, CancellationToken cancellation = default);

    Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default);
}
