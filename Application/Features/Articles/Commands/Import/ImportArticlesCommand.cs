using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Commands.Import;

public sealed record ImportArticlesCommand(
    Guid SourceId)
    : ICommand<Result>;