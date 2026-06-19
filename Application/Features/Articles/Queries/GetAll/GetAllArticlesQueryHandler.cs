using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Shared;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

internal sealed class GetAllArticlesQueryHandler(
    INewsAggregatorDbContext _context)
    : IQueryHandler<
        GetAllArticlesQuery, Result<ArticlesResponse>>
{
    public async ValueTask<Result<ArticlesResponse>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _context.Articles
            .Include(x => x.Source)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return Result<ArticlesResponse>
            .Success(new ArticlesResponse(
                request.PageNumber,
                request.PageSize,
                articles.Select(ArticleMapper.ToResponse))
            );
    }
}