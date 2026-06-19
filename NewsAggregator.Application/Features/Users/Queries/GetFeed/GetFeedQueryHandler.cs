using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

namespace NewsAggregator.Application.Features.Users.Queries.GetFeed;

internal sealed class GetFeedQueryHandler(
    INewsAggregatorDbContext _context)
    : IQueryHandler<
        GetFeedQuery, Result<List<ArticleResponse>>>
{
    public async ValueTask<Result<List<ArticleResponse>>> Handle(GetFeedQuery query, CancellationToken cancellationToken)
    {
        var preferences =
            await _context
                .UserCategoryPreferences
                .Where(
                    x =>
                        x.UserId ==
                        query.UserId)
                .ToListAsync(
                    cancellationToken);

        var scores =
            preferences.ToDictionary(
                x => x.CategoryId,
                x => x.Weight);

        var articles =
            await _context.Articles
                .Include(
                    x => x.ArticleCategories)
                .Include(
                    x => x.Source)
                .OrderByDescending(
                    x => x.PublishedAt)
                .ToListAsync(
                    cancellationToken);

        var result =
            articles
                .Select(
                    article =>
                    {
                        var score =
                            article
                                .ArticleCategories
                                .Sum(
                                    category =>
                                        scores
                                            .TryGetValue(
                                                category.CategoryId,
                                                out var weight)
                                            ? weight
                                            : 0);

                        return new
                        {
                            Article = article,
                            Score = score
                        };
                    })
                .OrderByDescending(
                    x => x.Score)
                .ThenByDescending(
                    x => x.Article.PublishedAt)
                .Take(50)
                .Select(
                    x =>
                        new ArticleResponse(
                            x.Article.Id,
                            x.Article.Title,
                            x.Article.Description,
                            x.Article.Source.Name
                            )).ToList();

        return Result<
            List<ArticleResponse>>
            .Success(result);
    }
}