using Mediator;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Features.Articles.Commands.Import;
using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;
using NewsAggregator.Application.Features.Users.Commands.LikeArticle;
using NewsAggregator.WebAPI.Common.Mappings;
using NewsAggregator.WebAPI.Contracts.Articles;
using NewsAggregator.WebAPI.Extensions;

namespace NewsAggregator.WebAPI.Endpoints;

internal static class ArticleEndpoints
{
    internal static IEndpointRouteBuilder
        MapArticleEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/articles");

        group.MapPost(
            "/",
            CreateArticle);

        group.MapPost(
            "/import",
            ImportArticles);

        group.MapPost(
            "/{articleId:guid}/like",
            LikeArticle)
            .RequireAuthorization();

        group.MapGet(
            "/",
            GetAllArticles);

        group.MapGet(
            "/{id:guid}",
            GetArticleById);

        return app;
    }

    private static async Task<IResult> CreateArticle(
        CreateArticleRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            request.ToCommand(),
            cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult> ImportArticles(
        ImportArticlesRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new ImportArticlesCommand(
                request.SourceId),
            cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult> LikeArticle(
        Guid articleId,
        IUserContext userContext,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new LikeArticleCommand(
                    userContext.UserId,
                    articleId),
                cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult> GetAllArticles(
        [AsParameters] GetAllArticlesRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            request.ToQuery(),
            cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult> GetArticleById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(
                new GetArticleByIdQuery(id),
                cancellationToken);

        return result.ToHttpResult();
    }
}