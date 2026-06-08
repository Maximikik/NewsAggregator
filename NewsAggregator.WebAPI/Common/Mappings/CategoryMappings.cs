using NewsAggregator.Application.Features.Categories.Commands.Create;
using NewsAggregator.WebAPI.Contracts.Categories;

namespace NewsAggregator.WebAPI.Common.Mappings;

internal static class CategoryMappings
{
    internal static CreateCategoryCommand ToCommand(
        this CreateCategoryRequest request)
    {
        return new CreateCategoryCommand(
            request.Name);
    }
}
