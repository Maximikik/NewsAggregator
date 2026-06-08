using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Categories.Queries.GetById;

public sealed record GetCategoryByIdQuery(
    Guid Id)
    : IRequest<Result<CategoryResponse>>;
