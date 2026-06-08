using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Users.Commands.Login
{
    public sealed class LoginUserCommandHandler
       : ICommandHandler<
           LoginUserCommand,
           Result<string>>
    {
        private readonly INewsAggregatorDbContext _context;

        private readonly IJwtTokenGenerator _jwt;

        public LoginUserCommandHandler(
            INewsAggregatorDbContext context,
            IJwtTokenGenerator jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async ValueTask<Result<string>> Handle(
           LoginUserCommand command,
           CancellationToken cancellationToken)
        {
            var user =
                await _context.Users
                    .FirstOrDefaultAsync(
                        x => x.Email == command.Email,
                        cancellationToken);

            if (user is null)
            {
                return Result<string>.Failure(
                    UserErrors.InvalidCredentials);
            }

            var valid =
                BCrypt.Net.BCrypt.Verify(
                    command.Password,
                    user.PasswordHash);

            if (!valid)
            {
                return Result<string>.Failure(
                    UserErrors.InvalidCredentials);
            }

            var token =
                _jwt.Generate(user);

            return Result<string>
                .Success(token);
        }
    }
}
