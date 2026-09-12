namespace NewsAggregator.Application.Common.Authentication;

public sealed record GeneratedToken(
    string AccessToken,
    int ExpiresInSeconds);