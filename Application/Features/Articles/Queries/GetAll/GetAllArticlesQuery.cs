using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

public sealed record GetAllArticlesQuery(
    int pageNumber, int pageSize)
    : IRequest<Result<ArticlesResponse>>;
