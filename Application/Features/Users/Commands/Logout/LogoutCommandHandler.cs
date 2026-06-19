using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Users.Commands.Logout;

internal sealed class LogoutCommandHandler(
    INewsAggregatorDbContext _context)
    : ICommandHandler<LogoutCommand, Result>
{
    public async ValueTask<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
            x => x.Token == request.RefreshToken);

        if (refreshToken is null)
        {
            return Result.Failure(
                new Error(
                    "RefreshTokens.NotFound",
                    "Refresh token not found"));
        }

        refreshToken.Revoke();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}