using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Entities;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Users.Events;

public sealed class ArticleLikedEventHandler
    : IDomainEventHandler<ArticleLikedEvent>
{
    private readonly INewsAggregatorDbContext
        _context;

    public ArticleLikedEventHandler(
        INewsAggregatorDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        ArticleLikedEvent notification,
        CancellationToken cancellationToken)
    {
        var article =
            await _context.Articles
                .Include(
                    x => x.ArticleCategories)
                .FirstOrDefaultAsync(
                    x =>
                        x.Id ==
                        notification.ArticleId,
                    cancellationToken);

        if (article is null)
        {
            return;
        }

        foreach (var category
                 in article.ArticleCategories)
        {
            var preference =
                await _context
                    .UserCategoryPreferences
                    .FirstOrDefaultAsync(
                        x =>
                            x.UserId ==
                            notification.UserId &&
                            x.CategoryId ==
                            category.CategoryId,
                        cancellationToken);

            if (preference is null)
            {
                _context
                    .UserCategoryPreferences
                    .Add(
                        new UserCategoryPreference(
                            notification.UserId,
                            category.CategoryId));
            }
            else
            {
                preference.IncreaseWeight();
            }
        }

        await _context.SaveChangesAsync(
            cancellationToken);
    }
}
