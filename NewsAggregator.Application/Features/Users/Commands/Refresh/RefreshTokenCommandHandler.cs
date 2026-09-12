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
        var refreshToken =
            await _context.RefreshTokens
                .Include(x => x.User)
                    .SingleOrDefaultAsync(
                        x => x.Token == command.RefreshToken,
                    cancellationToken);

        if (refreshToken is null)
        {
            return Result<LoginResponse>
                .Failure(
                    UserErrors.InvalidCredentials);
        }

        if (!refreshToken.IsActive)
        {
            return Result<LoginResponse>
                .Failure(
                    UserErrors.InvalidCredentials);
        }

        refreshToken.Revoke();

        var generatedRefreshToken =
            _refreshTokenGenerator
                .Generate();

        var newRefresh =
            new RefreshToken(
                refreshToken.UserId,
                generatedRefreshToken.Value,
                generatedRefreshToken.ExpiresAtUtc);

        _context.RefreshTokens
            .Add(newRefresh);

        var generatedAccessToken =
            _jwtTokenGenerator.Generate(refreshToken.User);

        await _context
            .SaveChangesAsync(cancellationToken);

        return Result<LoginResponse>
            .Success(
                new LoginResponse(
                    generatedAccessToken.AccessToken,
                    generatedAccessToken.ExpiresInSeconds,
                    generatedRefreshToken.Value));
    }
}
