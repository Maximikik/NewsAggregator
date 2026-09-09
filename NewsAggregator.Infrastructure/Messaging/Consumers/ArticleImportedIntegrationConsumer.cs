using MassTransit;
using Microsoft.Extensions.Logging;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Infrastructure.Messaging.Consumers;

internal sealed class ArticleImportedIntegrationConsumer(
    ILogger<ArticleImportedIntegrationConsumer> logger)
    : IConsumer<ArticleImportedEvent>
{
    public Task Consume(ConsumeContext<ArticleImportedEvent> context)
    {
        logger.LogInformation(
            "[RabbitMQ] Received ArticleImportedEvent off the bus: {Title} ({ArticleId})",
            context.Message.Title,
            context.Message.ArticleId);

        return Task.CompletedTask;
    }
}
