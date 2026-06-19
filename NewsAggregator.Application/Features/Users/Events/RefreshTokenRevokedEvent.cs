using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Application.Features.Users.Events;

internal sealed class RefreshTokenRevokedEventHandler(
    ILogger<RefreshTokenRevokedEvent> _logger)
    : IDomainEventHandler<RefreshTokenRevokedEvent>
{
    public Task Handle(RefreshTokenRevokedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Refresh token {RefreshTokenId} revoked, userId: {UserId}",
            notification.RefreshTokenId,
            notification.UserId);

        return Task.CompletedTask;
    }
}