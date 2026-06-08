namespace NewsAggregator.WebAPI.Contracts.Articles;

public sealed record ImportArticlesRequest(
    Guid SourceId,
    string FeedUrl);
