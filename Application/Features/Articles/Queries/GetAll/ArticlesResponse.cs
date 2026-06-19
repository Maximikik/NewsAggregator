using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

public sealed record ArticlesResponse(
    int PageNumber,
    int PageSize,
    IEnumerable<ArticleResponse> articles);
