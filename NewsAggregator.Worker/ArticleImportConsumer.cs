using System.Text;
using System.Text.Json;
using Mediator;
using NewsAggregator.Application.Features.Articles.Commands.Import;
using NewsAggregator.Infrastructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NewsAggregator.Worker;

public sealed class ArticleImportConsumer(
    IChannel _channel,
    IServiceScopeFactory _scopeFactory,
    ILogger<ArticleImportConsumer> _logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, args) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var command = JsonSerializer.Deserialize<ImportArticlesCommand>(json);

                if (command is null)
                {
                    _logger.LogError("Received an empty or invalid import message, sending to DLQ");

                    await _channel.BasicNackAsync(args.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
                    return;
                }

                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(command, stoppingToken);

                await _channel.BasicAckAsync(args.DeliveryTag, multiple: false, cancellationToken: stoppingToken);

                _logger.LogInformation("Imported source {SourceId}", command.SourceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process import message, sending to DLQ");

                await _channel.BasicNackAsync(args.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: RabbitMqTopology.ArticleImportQueue,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}