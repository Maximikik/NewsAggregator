using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Users.Commands.Login;

internal sealed class LoginUserCommandHandler(
    INewsAggregatorDbContext _context,
    IJwtTokenGenerator _jwtTokenGenerator,
    IRefreshTokenGenerator _refreshTokenGenerator)
   : ICommandHandler<
       LoginUserCommand, Result<LoginResponse>>
{
    public async ValueTask<Result<LoginResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user =
            await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == command.Email,
                    cancellationToken);

        if (user is null)
        {
            return Result<LoginResponse>.Failure(
                UserErrors.InvalidCredentials);
        }

        var valid =
            BCrypt.Net.BCrypt.Verify(
                command.Password,
                user.PasswordHash);

        if (!valid)
        {
            return Result<LoginResponse>.Failure(
                UserErrors.InvalidCredentials);
        }

        var accessToken =
            _jwtTokenGenerator.Generate(user);

        var refreshTokenValue =
            _refreshTokenGenerator.Generate();

        var refreshToken =
            new RefreshToken(
                user.Id,
                refreshTokenValue,
                DateTime.UtcNow.AddHours(1));

        _context.RefreshTokens.Add(
            refreshToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        return Result<LoginResponse>
            .Success(
                new LoginResponse(
                    accessToken,
                    900,
                    refreshTokenValue)
                );
    }
}