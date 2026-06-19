using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Sources.Create;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Sources.Commands.Create;

internal sealed class CreateSourceCommandHandler(
    INewsAggregatorDbContext _context,
    ICacheService _cache)
    : IRequestHandler<
        CreateSourceCommand, Result<CreateSourceResponse>>
{
    public async ValueTask<Result<CreateSourceResponse>> Handle(CreateSourceCommand command, CancellationToken cancellationToken)
    {
        var exists =
            await _context.Sources
                .AnyAsync(
                    x => x.Name == command.Name,
                    cancellationToken);

        if (exists)
        {
            return Result<CreateSourceResponse>
                .Failure(
                    Errors.Conflict(
                        "Source already exists"));
        }

        var source =
            new Source(
                command.Name,
                command.BaseUrl);

        _context.Sources.Add(source);

        await _context.SaveChangesAsync(
            cancellationToken);

        _cache.Remove("sources");

        return Result<CreateSourceResponse>
            .Success(
                new CreateSourceResponse(
                    source.Id));
    }
}