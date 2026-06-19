namespace NewsAggregator.WebAPI.Contracts.Articles;

internal sealed record ImportArticlesRequest(
    Guid SourceId);
