using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Sources.Queries.GetAll;

public sealed record GetAllSourcesQuery()
    : IQuery<Result<SourcesReponse>>;