using RabbitMQ.Client;

namespace NewsAggregator.Infrastructure.Messaging;

public static class RabbitMqTopology
{
    public const string Exchange = "news-aggregator.direct";
    public const string ArticleImportQueue = "article-import-queue";
    public const string ArticleImportRoutingKey = "article.import";
    public const string DeadLetterExchange = "news-aggregator.dlx";
    public const string ArticleIMportDeadLetterQueue = "article-import-queue.dead";

    public static async Task DeclareAsync(IChannel channel)
    {
        await channel.ExchangeDeclareAsync(DeadLetterExchange, ExchangeType.Fanout, durable: true);

        await channel.QueueDeclareAsync(ArticleIMportDeadLetterQueue, durable: true, exclusive: false, autoDelete: false);

        await channel.QueueBindAsync(ArticleIMportDeadLetterQueue, DeadLetterExchange, routingKey: "");

        await channel.ExchangeDeclareAsync(Exchange, ExchangeType.Direct, durable: true);

        await channel.QueueDeclareAsync(
            queue: ArticleImportQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: new Dictionary<string, object?>
            {
                ["x-dead-letter-exchange"] = DeadLetterExchange
            });

        await channel.QueueBindAsync(ArticleImportQueue, Exchange, ArticleImportRoutingKey);
    }
}
