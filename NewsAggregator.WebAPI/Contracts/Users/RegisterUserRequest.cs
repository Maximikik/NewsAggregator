namespace NewsAggregator.WebAPI.Contracts.Users;

internal sealed record RegisterUserRequest(
    string Email,
    string Password);
