using NewsAggregator.Application.Features.Sources.Queries.GetById;
using NewsAggregator.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace NewsAggregator.Application.Features.Sources.Shared;

[Mapper]
public static partial class SourceMapper
{
    public static partial SourceResponse ToResponse(Source source);
}