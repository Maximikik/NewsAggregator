using MassTransit;
using NewsAggregator.Application.Common.Interfaces;

namespace NewsAggregator.Infrastructure.Messaging;

internal sealed class MassTransitEventBus(
    IPublishEndpoint publishEndpoint)
    : IEventBus
{
    public Task PublishAsync(object @event, CancellationToken cancellationToken = default)
        => publishEndpoint.Publish(@event, cancellationToken);
}
