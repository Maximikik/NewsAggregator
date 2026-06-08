using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record ArticleImportedEvent(
    Guid ArticleId,
    string Title,
    Guid SourceId)
    : DomainEvent;