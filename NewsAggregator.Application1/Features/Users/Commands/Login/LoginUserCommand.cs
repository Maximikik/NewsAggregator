using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Users.Commands.Login;

public sealed record LoginUserCommand(
    string Email,
    string Password)
    : ICommand<Result<LoginResponse>>;
