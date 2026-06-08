using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Users.Commands.LikeArticle;

internal sealed class LikeArticleCommandHandler
    : ICommandHandler<
        LikeArticleCommand,
        Result>
{
    private readonly INewsAggregatorDbContext
        _context;

    public LikeArticleCommandHandler(
        INewsAggregatorDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Result> Handle(LikeArticleCommand command, CancellationToken cancellationToken)
    {
        var exists =
            await _context.UserArticleLikes
                .AnyAsync(
                    x =>
                        x.UserId == command.UserId &&
                        x.ArticleId == command.ArticleId,
                    cancellationToken);

        if (exists)
        {
            return Result.Success();
        }

        var article =
            await _context.Articles
                .Include(
                    x => x.ArticleCategories)
                .FirstOrDefaultAsync(
                    x => x.Id == command.ArticleId,
                    cancellationToken);

        if (article is null)
        {
            return Result.Failure(
                new Error(
                    "Articles.NotFound",
                    "Article not found"));
        }

        var like =
            new UserArticleLike(
                command.UserId,
                command.ArticleId);

        _context.UserArticleLikes
            .Add(like);

        foreach (var category
                 in article.ArticleCategories)
        {
            var preference =
                await _context
                    .UserCategoryPreferences
                    .FirstOrDefaultAsync(
                        x =>
                            x.UserId ==
                            command.UserId &&
                            x.CategoryId ==
                            category.CategoryId,
                        cancellationToken);

            if (preference is null)
            {
                preference =
                    new UserCategoryPreference(
                        command.UserId,
                        category.CategoryId);

                _context
                    .UserCategoryPreferences
                    .Add(preference);
            }
            else
            {
                preference.IncreaseWeight();
            }
        }

        await _context.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}