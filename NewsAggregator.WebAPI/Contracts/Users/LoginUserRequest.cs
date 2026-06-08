namespace NewsAggregator.WebAPI.Contracts.Users;

public sealed record LoginUserRequest(
    string Email,
    string Password);
