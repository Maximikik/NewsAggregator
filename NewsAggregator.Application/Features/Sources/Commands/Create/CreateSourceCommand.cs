using Mediator;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Sources.Commands.Create;

namespace NewsAggregator.Application.Features.Sources.Create;

public sealed record CreateSourceCommand(
    string Name,
    string BaseUrl)
    : IRequest<Result<CreateSourceResponse>>;