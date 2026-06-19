using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Commands.Import;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Articles.Import;

internal sealed class ImportArticlesCommandHandler(
    INewsAggregatorDbContext _context,
    IRssParser _rssParser)
    : ICommandHandler<
        ImportArticlesCommand, Result>
{
    public async ValueTask<Result> Handle(ImportArticlesCommand command, CancellationToken cancellationToken)
    {
        var source =
            await _context.Sources
                .FirstOrDefaultAsync(
                    x =>
                        x.Id ==
                        command.SourceId,
                    cancellationToken);

        if (source is null)
        {
            return Result.Failure(
                Errors.NotFound(
                    "Source not found"));
        }

        var rssArticles =
            await _rssParser.ParseAsync(
                source.BaseUrl,
                cancellationToken);

        var categories =
            await _context.Categories
                .ToDictionaryAsync(
                    x => x.Name,
                    cancellationToken);

        foreach (var rssArticle in rssArticles)
        {
            var exists =
                await _context.Articles
                    .AnyAsync(
                        x =>
                            x.Url ==
                            rssArticle.Url,
                        cancellationToken);

            if (exists)
            {
                continue;
            }

            var article =
                new Article(
                    rssArticle.Title,
                    rssArticle.Description,
                    rssArticle.Url,
                    rssArticle.PublishedAt,
                    source.Id);

            foreach (var categoryName
                in rssArticle.Categories)
            {
                if (!categories.TryGetValue(
                    categoryName,
                    out var category))
                {
                    category =
                        new Category(
                            categoryName);

                    categories.Add(
                        categoryName,
                        category);

                    _context.Categories.Add(
                        category);
                }

                article.AddCategory(
                    category);
            }

            _context.Articles.Add(
                article);
        }

        await _context.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}