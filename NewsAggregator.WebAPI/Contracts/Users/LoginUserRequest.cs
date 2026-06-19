namespace NewsAggregator.WebAPI.Contracts.Users;

internal sealed record LoginUserRequest(
    string Email,
    string Password);
