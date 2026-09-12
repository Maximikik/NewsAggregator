namespace NewsAggregator.Application.Common.Authentication;

public sealed record GeneratedRefreshToken(
    string Value,
    DateTime ExpiresAtUtc);
