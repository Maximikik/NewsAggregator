using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Articles.Shared;

namespace NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

internal sealed class GetArticleByIdHandler(
    INewsAggregatorDbContext _context)
    : IRequestHandler<
        GetArticleByIdQuery, Result<ArticleResponse>>
{
    public async ValueTask<Result<ArticleResponse>> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
    {
        var article =
            await _context.Articles
                .Include(x => x.Source)
                .Include(x => x.ArticleCategories)
                .FirstOrDefaultAsync(
                    x => x.Id == query.Id,
                    cancellationToken);

        if (article is null)
        {
            return Result<ArticleResponse>
                .Failure(
                    Errors.NotFound("Article"));
        }

        return Result<ArticleResponse>
            .Success(
                ArticleMapper.ToResponse(article));
    }
}
