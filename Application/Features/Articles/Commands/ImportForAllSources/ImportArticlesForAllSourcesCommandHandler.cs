using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Commands.Import;

namespace NewsAggregator.Application.Features.Articles.Commands.ImportForAllSources;

internal sealed class ImportArticlesForAllSourcesCommandHandler(
    INewsAggregatorDbContext _context,
    IMediator _mediator)
    : IRequestHandler<ImportArticlesForAllSourcesCommand, Result>
{
    public async ValueTask<Result> Handle(ImportArticlesForAllSourcesCommand request, CancellationToken cancellationToken)
    {
        var sourceIds =
            await _context.Sources
                .Where(x => x.IsActive)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

        foreach (var sourceId in sourceIds)
        {
            await _mediator.Send(
                new ImportArticlesCommand(
                    sourceId),
                cancellationToken);
        }

        return Result.Success();
    }
}
