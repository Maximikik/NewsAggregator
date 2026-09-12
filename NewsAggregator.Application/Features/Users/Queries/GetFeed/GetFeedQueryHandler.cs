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
            _context
                .UserCategoryPreferences
                .Where(
                    x =>
                        x.UserId ==
                        query.UserId);

        var result = await _context.Articles
            .Select(article => new
            {
                Article = article,
                Score = (
                    from ac in article.ArticleCategories
                    join p in preferences on ac.CategoryId equals p.CategoryId into matched
                    from p in matched.DefaultIfEmpty()
                    select p == null ? 0 : p.Weight
                ).Sum()
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Article.PublishedAt)
            .Take(50)
            .Select(x => new ArticleResponse(
                x.Article.Id,
                x.Article.Title,
                x.Article.Description,
                x.Article.Source.Name))
            .ToListAsync(cancellationToken);

        return Result<List<ArticleResponse>>
            .Success(result);
    }
}