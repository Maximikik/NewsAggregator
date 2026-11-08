namespace NewsAggregator.Application.Common.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}
