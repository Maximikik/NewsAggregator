using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

public sealed record GetAllArticlesQuery(
    int PageNumber,
    int PageSize)
    : IRequest<Result<ArticlesResponse>>;