using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Common;

namespace NewsAggregator.Infrastructure.Persistence;

public sealed class DomainEventDispatcher(
    IServiceProvider serviceProvider)
    : IDomainEventDispatcher
{
    public async Task DispatchAsync(
        DomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var handlerType =
            typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType());

        var handlers =
            serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var method =
                handlerType.GetMethod(nameof(IDomainEventHandler<>.Handle));

            await (Task)method!.Invoke(
                handler,
                [domainEvent, cancellationToken])!;
        }
    }
}