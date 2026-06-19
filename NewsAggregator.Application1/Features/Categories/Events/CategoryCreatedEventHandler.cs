using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Categories.Events;

internal sealed class CategoryCreatedEventHandler(
    ILogger<CategoryCreatedEventHandler> logger)
    : IDomainEventHandler<CategoryCreatedEvent>
{
    public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Category created: {Name}",
            notification.Name);

        return Task.CompletedTask;
    }
}