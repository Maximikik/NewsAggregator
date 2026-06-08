using NewsAggregator.Domain.Common;

namespace NewsAggregator.Application.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(
        DomainEvent domainEvent,
        CancellationToken cancellationToken = default);
}