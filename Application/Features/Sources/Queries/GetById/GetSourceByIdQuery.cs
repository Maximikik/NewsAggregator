using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Sources.Queries.GetById;

public sealed record GetSourceByIdQuery(
    Guid Id)
    : IRequest<Result<SourceResponse>>;
