using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record ArticleCategoryAssignedEvent(
    Guid ArticleId,
    Guid CategoryId)
    : DomainEvent;