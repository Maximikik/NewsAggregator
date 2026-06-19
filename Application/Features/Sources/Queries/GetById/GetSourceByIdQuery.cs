using Mediator;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Sources.Queries.GetById;

public sealed record GetSourceByIdQuery(
    Guid Id)
    : IQuery<Result<SourceResponse>>,
    ICacheableQuery
{
    public string CacheKey =>
        $"{CacheKeys.Sources}:{Id}";

    public TimeSpan Expiration =>
        TimeSpan.FromMinutes(10);
}