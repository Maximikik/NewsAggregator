namespace NewsAggregator.WebAPI.Contracts.Articles;

public sealed record CreateArticleRequest(
    string Title,
    string Description,
    Guid SourceId);
