using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record ArticleCreatedEvent(
    Guid ArticleId,
    string Title,
    Guid SourceId)
    : DomainEvent;