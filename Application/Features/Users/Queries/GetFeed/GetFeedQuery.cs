using Mediator;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

namespace NewsAggregator.Application.Features.Users.Queries.GetFeed;

public sealed record GetFeedQuery(
    Guid UserId)
    : IQuery<Result<List<ArticleResponse>>>;
