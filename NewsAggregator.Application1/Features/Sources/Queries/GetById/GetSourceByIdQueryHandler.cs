using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Sources.Shared;

namespace NewsAggregator.Application.Features.Sources.Queries.GetById;

internal sealed class GetSourceByIdHandler(
    INewsAggregatorDbContext _context)
    : IQueryHandler<
        GetSourceByIdQuery, Result<SourceResponse>>
{
    public async ValueTask<Result<SourceResponse>> Handle(GetSourceByIdQuery query, CancellationToken cancellationToken)
    {
        var source = await _context.Sources
                .FirstOrDefaultAsync(
                    x => x.Id == query.Id,
                    cancellationToken);

        if (source is null)
        {
            return Result<SourceResponse>
                .Failure(
                    Errors.NotFound("Source"));
        }

        return Result<SourceResponse>
            .Success(
                SourceMapper.ToResponse(source)
                );
    }
}