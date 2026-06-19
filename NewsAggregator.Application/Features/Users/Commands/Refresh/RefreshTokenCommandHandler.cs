using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Users.Commands.Login;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Users.Commands.Refresh;

public sealed class RefreshTokenCommandHandler(
    INewsAggregatorDbContext _context,
    IJwtTokenGenerator _jwtTokenGenerator,
    IRefreshTokenGenerator _refreshTokenGenerator)
    : ICommandHandler<RefreshTokenCommand,
        Result<LoginResponse>>
{
    public async ValueTask<Result<LoginResponse>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var token =
            await _context.RefreshTokens
                .Include(x => x.User)
                    .SingleOrDefaultAsync(
                        x => x.Token == command.RefreshToken,
                    cancellationToken);

        if (token is null)
        {
            return Result<LoginResponse>
                .Failure(
                    UserErrors.InvalidCredentials);
        }

        if (!token.IsActive)
        {
            return Result<LoginResponse>
                .Failure(
                    UserErrors.InvalidCredentials);
        }

        token.Revoke();

        var newRefreshValue =
            _refreshTokenGenerator
                .Generate();

        var newRefresh =
            new RefreshToken(
                token.UserId,
                newRefreshValue,
                DateTime.UtcNow
                    .AddDays(30));

        _context.RefreshTokens
            .Add(newRefresh);

        var accessToken =
            _jwtTokenGenerator.Generate(token.User);

        await _context
            .SaveChangesAsync(cancellationToken);

        return Result<LoginResponse>
            .Success(
                new LoginResponse(
                    accessToken,
                    900,
                    newRefreshValue));
    }
}
