using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record SourceCreatedEvent(
    Guid SourceId,
    string Name) : DomainEvent;