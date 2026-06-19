using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Sources.Shared;

namespace NewsAggregator.Application.Features.Sources.Queries.GetAll;

internal sealed class GetAllSourcesQueryHandler(
    INewsAggregatorDbContext _context)
    : IQueryHandler<GetAllSourcesQuery, Result<SourcesReponse>>
{
    public async ValueTask<Result<SourcesReponse>> Handle(GetAllSourcesQuery request, CancellationToken cancellationToken)
    {
        var sources = await _context.Sources.ToListAsync(cancellationToken);

        return Result<SourcesReponse>
            .Success(new SourcesReponse(
                sources.Select(SourceMapper.ToResponse))
            );
    }
}
