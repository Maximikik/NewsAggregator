using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Users.Commands.Register;

internal sealed class RegisterUserCommandHandler
    : ICommandHandler<
        RegisterUserCommand, Result<Guid>>
{
    private readonly INewsAggregatorDbContext _context;

    public RegisterUserCommandHandler(
        INewsAggregatorDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Result<Guid>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var exists =
            await _context.Users.AnyAsync(
                x => x.Email == command.Email,
                cancellationToken);

        if (exists)
        {
            return Result<Guid>.Failure(
                UserErrors.AlreadyExists);
        }

        var user =
            new User(
                command.Email,
                BCrypt.Net.BCrypt.HashPassword(
                    command.Password));

        _context.Users.Add(user);

        await _context.SaveChangesAsync(
            cancellationToken);

        return Result<Guid>.Success(
            user.Id);
    }
}
