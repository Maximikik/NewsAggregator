using NewsAggregator.Application.Common.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace NewsAggregator.Infrastructure.Messaging;

public sealed class RabbitMqMessagePublisher(
    IChannel _channel
    ) : IMessagePublisher
{
    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(
            exchange: RabbitMqTopology.Exchange,
            routingKey: RabbitMqTopology.ArticleImportRoutingKey,
            mandatory: false,
            basicProperties: new BasicProperties { Persistent = true },
            body: body,
            cancellationToken: cancellationToken);
    }
}
