namespace NewsAggregator.WebAPI.Contracts.Articles;

internal sealed record CreateArticleRequest(
    string Title,
    string Description,
    Guid SourceId);
