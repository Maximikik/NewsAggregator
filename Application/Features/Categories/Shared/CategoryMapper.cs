using NewsAggregator.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace NewsAggregator.Application.Features.Categories.Shared;

[Mapper]
public static partial class CategoryMapper
{
    public static partial CategoryDto ToDto(
        Category source);
}