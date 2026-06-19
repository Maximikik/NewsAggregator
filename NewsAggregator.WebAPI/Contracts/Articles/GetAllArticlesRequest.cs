namespace NewsAggregator.WebAPI.Contracts.Articles;

internal sealed record GetAllArticlesRequest(
    int PageNumber,
    int PageSize);
