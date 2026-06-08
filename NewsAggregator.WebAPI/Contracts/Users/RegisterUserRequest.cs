namespace NewsAggregator.WebAPI.Contracts.Users;

public sealed record RegisterUserRequest(
    string Email,
    string Password);
