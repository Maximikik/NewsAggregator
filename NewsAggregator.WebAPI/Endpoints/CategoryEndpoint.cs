using Mediator;
using NewsAggregator.Application.Features.Categories.Queries.GetById;
using NewsAggregator.WebAPI.Common.Mappings;
using NewsAggregator.WebAPI.Contracts.Categories;
using NewsAggregator.WebAPI.Extensions;

namespace NewsAggregator.WebAPI.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder
        MapCategoryEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/categories");

        group.MapPost(
            "/",
            CreateCategory);

        group.MapGet(
            "/{id:guid}",
            GetCategoryById);

        return app;
    }

    private static async Task<IResult>
        CreateCategory(
        CreateCategoryRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                request.ToCommand(),
                cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult>
        GetCategoryById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new GetCategoryByIdQuery(id),
                cancellationToken);

        return result.ToHttpResult();
    }
}
