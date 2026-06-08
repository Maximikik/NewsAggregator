namespace NewsAggregator.WebAPI.Contracts.Articles;

public sealed record GetAllArticlesRequest(
    int PageNumber,
    int PageSize);
