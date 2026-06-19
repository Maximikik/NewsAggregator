using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Articles.Events;

internal sealed class ArticleCategoryAssignedEventHandler(
    ILogger<ArticleCategoryAssignedEventHandler> logger)
    : IDomainEventHandler<ArticleCategoryAssignedEvent>
{
    public Task Handle(ArticleCategoryAssignedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Article category assigned: {CategoryId}",
            domainEvent.CategoryId);

        return Task.CompletedTask;
    }
}
