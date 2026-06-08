using NewsAggregator.WebAPI.Endpoints;

namespace NewsAggregator.WebAPI.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(
        this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapArticleEndpoints();
        endpointRouteBuilder.MapSourceEndpoints();
        endpointRouteBuilder.MapCategoryEndpoints();
        endpointRouteBuilder.MapUserEndpoints();

        return endpointRouteBuilder;
    }
}
