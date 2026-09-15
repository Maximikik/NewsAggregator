using Mediator;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Features.Users.Commands.Logout;
using NewsAggregator.Application.Features.Users.Commands.Refresh;
using NewsAggregator.Application.Features.Users.Queries.GetFeed;
using NewsAggregator.WebAPI.Common.Mappings;
using NewsAggregator.WebAPI.Contracts.Users;
using NewsAggregator.WebAPI.Extensions;

namespace NewsAggregator.WebAPI.Endpoints;

internal static class UserEndpoints
{
    internal static IEndpointRouteBuilder
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

        group.MapPost(
            "/refresh",
            Refresh);

        group.MapPost(
            "/logout",
            Logout);

        group.MapGet(
            "/feed",
            GetFeed)
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Register(
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

    private static async Task<IResult> Login(
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

    private static async Task<IResult> Refresh(
       RefreshRequest request,
       IMediator mediator,
       CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new RefreshTokenCommand(request.RefreshToken),
                cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult> Logout(
       LogoutRequest request,
       IMediator mediator,
       CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new LogoutCommand(request.RefreshToken),
                cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult> GetFeed(
        IMediator mediator,
        IUserContext userContext,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new GetFeedQuery(
                    userContext.UserId),
                cancellationToken);

        return result.ToHttpResult();
    }
}
