using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Sources.Events;

public sealed class SourceCreatedEventHandler(
    ILogger<SourceCreatedEventHandler> logger)
    : IDomainEventHandler<SourceCreatedEvent>
{
    public Task Handle(
        SourceCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Source created: {Name}",
            notification.Name);

        return Task.CompletedTask;
    }
}