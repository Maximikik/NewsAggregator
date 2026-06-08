namespace NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

public sealed record ArticleResponse(
    Guid Id,
    string Title,
    string Description,
    string Source,
    IReadOnlyCollection<string> Categories);