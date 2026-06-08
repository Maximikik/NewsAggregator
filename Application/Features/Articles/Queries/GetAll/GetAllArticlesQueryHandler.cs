using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Shared;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

internal sealed class GetAllArticlesQueryHandler(
    INewsAggregatorDbContext context)
    : IRequestHandler<
        GetAllArticlesQuery, Result<ArticlesResponse>>
{
    public async ValueTask<Result<ArticlesResponse>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await context.Articles
            .Include(x => x.Source)
            .Skip((request.pageNumber - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return Result<ArticlesResponse>
            .Success(new ArticlesResponse(
                articles.Select(ArticleMapper.ToResponse))
            );
    }
}
