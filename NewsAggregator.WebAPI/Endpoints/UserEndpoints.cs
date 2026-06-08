using Mediator;
using NewsAggregator.WebAPI.Common.Mappings;
using NewsAggregator.WebAPI.Contracts.Users;
using NewsAggregator.WebAPI.Extensions;

namespace NewsAggregator.WebAPI.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder
        MapUserEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/users");

        group.MapPost(
            "/register",
            Register);

        group.MapPost(
            "/login",
            Login);

        return app;
    }

    private static async Task<IResult>
        Register(
        RegisterUserRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                request.ToRegisterCommand(),
                cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult>
        Login(
        LoginUserRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                request.ToLoginCommand(),
                cancellationToken);

        return result.ToHttpResult();
    }
}
