namespace NewsAggregator.WebAPI.Contracts.Users;

public sealed record LogoutRequest(
    string RefreshToken);
