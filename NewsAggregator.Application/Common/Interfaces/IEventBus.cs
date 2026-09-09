namespace NewsAggregator.Application.Common.Interfaces;

public interface IEventBus
{
    Task PublishAsync(object @event, CancellationToken cancellationToken = default);
}
