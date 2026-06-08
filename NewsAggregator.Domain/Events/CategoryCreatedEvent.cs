using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record CategoryCreatedEvent(
    Guid CategoryId,
    string Name)
    : DomainEvent;