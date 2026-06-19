using NewsAggregator.Application.Features.Categories.Queries.GetById;
using NewsAggregator.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace NewsAggregator.Application.Features.Categories.Shared;

[Mapper]
public static partial class CategoryMapper
{
    public static partial CategoryResponse ToResponse(Category source);
}