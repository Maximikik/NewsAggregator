using Mediator;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

public sealed record GetAllArticlesQuery(
    int PageNumber,
    int PageSize)
    : IQuery<Result<ArticlesResponse>>,
    ICacheableQuery
{
    public string CacheKey =>
      $"{CacheKeys.Articles}:{PageNumber}:{PageSize}";

    public TimeSpan Expiration =>
        TimeSpan.FromMinutes(5);
}