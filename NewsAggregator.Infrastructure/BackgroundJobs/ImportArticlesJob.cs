using Mediator;
using NewsAggregator.Application.Features.Articles.Commands.ImportForAllSources;

namespace NewsAggregator.Infrastructure.BackgroundJobs;

public sealed class ImportArticlesJob(
    IMediator mediator)
{
    public async Task Execute(
        CancellationToken cancellationToken)
    {
        await mediator.Send(
            new ImportArticlesForAllSourcesCommand(),
            cancellationToken);
    }
}
