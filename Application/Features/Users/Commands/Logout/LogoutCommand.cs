using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Users.Commands.Logout;

public sealed record LogoutCommand(
    string RefreshToken)
    : ICommand<Result>;
