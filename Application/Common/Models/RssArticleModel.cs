namespace NewsAggregator.Application.Common.Models;

public sealed record RssArticleModel(
    string Title,
    string Description,
    string Url,
    DateTime PublishedAt);