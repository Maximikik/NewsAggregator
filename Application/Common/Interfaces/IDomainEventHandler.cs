using NewsAggregator.Domain.Common;

namespace NewsAggregator.Application.Common.Interfaces;

public interface IDomainEventHandler<in TEvent>
    where TEvent : DomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}
