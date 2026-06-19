using Mediator;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Sources.Queries.GetAll;

public sealed record GetAllSourcesQuery()
    : IQuery<Result<SourcesReponse>>,
    ICacheableQuery
{
    public string CacheKey =>
        CacheKeys.Sources;

    public TimeSpan Expiration =>
        TimeSpan.FromMinutes(30);
}