using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Articles.Events;

internal sealed class ArticleCreatedEventHandler(
    ILogger<ArticleCreatedEventHandler> logger)
    : IDomainEventHandler<ArticleCreatedEvent>
{
    public Task Handle(ArticleCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Article created: {Title}",
            domainEvent.Title);

        return Task.CompletedTask;
    }
}