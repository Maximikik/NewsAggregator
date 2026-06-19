using Mediator;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Users.Commands.Login;

namespace NewsAggregator.Application.Features.Users.Commands.Refresh;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : ICommand<Result<LoginResponse>>;
