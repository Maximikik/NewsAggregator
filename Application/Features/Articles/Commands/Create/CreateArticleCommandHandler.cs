using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Articles.Commands.Create;

internal sealed class CreateArticleHandler(
    INewsAggregatorDbContext context)
    : IRequestHandler<
        CreateArticleCommand,
        Result<CreateArticleResponse>>
{
    public async ValueTask<
        Result<CreateArticleResponse>>
        Handle(
        CreateArticleCommand command,
        CancellationToken cancellationToken)
    {
        var sourceExists =
            await context.Sources
                .AnyAsync(
                    x => x.Id == command.SourceId,
                    cancellationToken);

        if (!sourceExists)
        {
            return Result<CreateArticleResponse>
                .Failure(
                    Errors.NotFound("Source"));
        }

        var article =
            new Article(
                command.Title,
                command.Description,
                string.Empty,
                DateTime.UtcNow,
                command.SourceId);

        context.Articles.Add(article);

        await context.SaveChangesAsync(
            cancellationToken);

        return Result<CreateArticleResponse>
            .Success(
                new CreateArticleResponse(
                    article.Id));
    }
}