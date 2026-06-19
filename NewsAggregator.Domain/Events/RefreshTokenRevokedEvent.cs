using NewsAggregator.Domain.Common;

namespace NewsAggregator.Domain.Events;

public sealed record RefreshTokenRevokedEvent(
    Guid UserId,
    Guid RefreshTokenId)
    : DomainEvent;