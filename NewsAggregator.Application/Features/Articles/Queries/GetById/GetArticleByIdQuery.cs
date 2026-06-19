using Mediator;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

public sealed record GetArticleByIdQuery(
    Guid Id)
    : IQuery<Result<ArticleResponse>>,
    ICacheableQuery
{
    public string CacheKey =>
        $"{CacheKeys.Articles}:{Id}";

    public TimeSpan Expiration =>
        TimeSpan.FromMinutes(5);
}