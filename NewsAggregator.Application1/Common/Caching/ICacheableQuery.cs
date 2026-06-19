using Mediator;

namespace NewsAggregator.Application.Common.Caching;

public interface ICacheableQuery : IMessage
{
    string CacheKey { get; }
    TimeSpan Expiration { get; }
}