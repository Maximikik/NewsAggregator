using Mediator;
using NewsAggregator.Application.Common.Results;


namespace NewsAggregator.Application.Features.Users.Commands.Register;

public sealed record RegisterUserCommand(
    string Email,
    string Password)
    : ICommand<Result<Guid>>;
