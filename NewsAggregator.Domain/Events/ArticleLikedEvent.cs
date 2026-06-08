using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record ArticleLikedEvent(
    Guid UserId,
    Guid ArticleId)
    : DomainEvent;
