namespace NewsAggregator.Infrastructure.Messaging;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    public string HostName { get; init; } = null!;

    public int Port { get; init; } = 5672;

    public string UserName { get; init; } = null!;

    public string Password { get; init; } = null!;
}