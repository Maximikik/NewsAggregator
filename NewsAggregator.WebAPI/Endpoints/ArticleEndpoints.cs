using Mediator;
using NewsAggregator.Application.Features.Articles.Commands.Import;
using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;
using NewsAggregator.WebAPI.Common.Mappings;
using NewsAggregator.WebAPI.Contracts.Articles;
using NewsAggregator.WebAPI.Extensions;

namespace NewsAggregator.WebAPI.Endpoints;

public static class ArticleEndpoints
{
    public static IEndpointRouteBuilder
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

        group.MapGet(
            "/",
            GetAllArticles);

        group.MapGet(
            "/{id:guid}",
            GetArticleById);

        return app;
    }

    private static async Task<IResult>
        CreateArticle(
        CreateArticleRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            request.ToCommand(),
            cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult>
        ImportArticles(
        ImportArticlesRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new ImportArticlesCommand(
            request.SourceId,
            request.FeedUrl);

        var result = await mediator.Send(
            command,
            cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult>
        GetAllArticles(
        [AsParameters] GetAllArticlesRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            request.ToQuery(),
            cancellationToken);

        return result.ToHttpResult();
    }

    private static async Task<IResult>
        GetArticleById(
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