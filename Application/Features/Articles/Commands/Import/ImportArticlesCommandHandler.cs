using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Commands.Import;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Articles.Import;

internal sealed class ImportArticlesHandler(
    INewsAggregatorDbContext context,
    IRssParser rssParser)
    : IRequestHandler<
        ImportArticlesCommand,
        Result>
{
    public async ValueTask<Result>
        Handle(
        ImportArticlesCommand command,
        CancellationToken cancellationToken)
    {
        var source =
            await context.Sources
                .FirstOrDefaultAsync(
                    x => x.Id == command.SourceId,
                    cancellationToken);

        if (source is null)
        {
            return Result.Failure(
                Errors.NotFound("Source"));
        }

        var rssArticles =
            await rssParser.ParseAsync(
                command.FeedUrl,
                cancellationToken);

        foreach (var rssArticle in rssArticles)
        {
            var exists =
                await context.Articles
                    .AnyAsync(
                        x => x.Url == rssArticle.Url,
                        cancellationToken);

            if (exists)
                continue;

            context.Articles.Add(
                new Article(
                    rssArticle.Title,
                    rssArticle.Description,
                    rssArticle.Url,
                    rssArticle.PublishedAt,
                    source.Id));
        }

        await context.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}