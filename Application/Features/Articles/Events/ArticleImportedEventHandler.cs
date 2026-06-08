using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Articles.Events;

public sealed class ArticleImportedEventHandler(
    ILogger<ArticleImportedEventHandler> logger)
    : IDomainEventHandler<ArticleImportedEvent>
{
    public Task Handle(
        ArticleImportedEvent notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Article imported: {Title}",
            notification.Title);

        return Task.CompletedTask;
    }
}