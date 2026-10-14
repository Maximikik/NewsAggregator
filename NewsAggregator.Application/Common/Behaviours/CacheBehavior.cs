using Mediator;
using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Caching;

namespace NewsAggregator.Application.Common.Behaviours;

internal sealed class CacheBehavior<TMessage, TResult>(
    ICacheService _cache,
    ILogger<CacheBehavior<TMessage, TResult>> _logger)
    : IPipelineBehavior<TMessage, TResult> where TMessage : ICacheableQuery
{
    public async ValueTask<TResult> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResult> next, CancellationToken cancellationToken)
    {
        var cached =
               await _cache.GetAsync<TResult>(
                   message.CacheKey,
                   cancellationToken);

        if (cached is not null)
        {
            _logger.LogInformation("Cache hit: {message}", message.CacheKey);
            return cached;
        }

        _logger.LogInformation("Cache miss: {message}", message.CacheKey);

        var result =
            await next(
                message,
                cancellationToken);

        await _cache.SetAsync(
            message.CacheKey,
            result,
            message.Expiration,
            cancellationToken);

        return result;
    }
}
