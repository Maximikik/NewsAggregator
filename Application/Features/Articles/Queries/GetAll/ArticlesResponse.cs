using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

public sealed record ArticlesResponse(
    IEnumerable<ArticleResponse> articles);
