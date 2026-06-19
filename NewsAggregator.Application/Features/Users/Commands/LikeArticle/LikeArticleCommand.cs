using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Users.Commands.LikeArticle;

public sealed record LikeArticleCommand(
    Guid UserId,
    Guid ArticleId)
    : ICommand<Result>;
