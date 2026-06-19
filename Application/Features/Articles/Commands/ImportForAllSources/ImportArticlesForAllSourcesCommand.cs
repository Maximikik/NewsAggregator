using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Commands.ImportForAllSources;

public sealed record ImportArticlesForAllSourcesCommand()
    : IRequest<Result>;
