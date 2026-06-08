using Mediator;
using NewsAggregator.Application.Features.Sources.Queries.GetById;
using NewsAggregator.WebAPI.Common.Mappings;
using NewsAggregator.WebAPI.Contracts.Sources;
using NewsAggregator.WebAPI.Extensions;

namespace NewsAggregator.WebAPI.Endpoints;

public static class SourceEndpoints
{
    public static IEndpointRouteBuilder
        MapSourceEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/sources");

        group.MapPost(
            "/",
            CreateSource);

        group.MapGet(
            "/{id:guid}",
            GetSourceById);

        return app;
    }

    private static async Task<IResult>
        CreateSource(
        CreateSourceRequest request,
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
        GetSourceById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new GetSourceByIdQuery(id),
                cancellationToken);

        return result.ToHttpResult();
    }
}
