using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

public sealed record GetArticleByIdQuery(
    Guid Id)
    : IRequest<Result<ArticleResponse>>;