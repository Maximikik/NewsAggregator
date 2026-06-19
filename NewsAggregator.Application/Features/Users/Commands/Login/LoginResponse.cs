namespace NewsAggregator.Application.Features.Users.Commands.Login;

public sealed record LoginResponse(
    string AccessToken,
    int ExpiresIn,
    string RefreshToken);
