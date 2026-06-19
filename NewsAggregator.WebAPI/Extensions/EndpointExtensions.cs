using NewsAggregator.WebAPI.Endpoints;

namespace NewsAggregator.WebAPI.Extensions;

internal static class EndpointExtensions
{
    internal static IEndpointRouteBuilder MapEndpoints(
        this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapArticleEndpoints();
        endpointRouteBuilder.MapSourceEndpoints();
        endpointRouteBuilder.MapCategoryEndpoints();
        endpointRouteBuilder.MapUserEndpoints();

        return endpointRouteBuilder;
    }
}
