using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Articles.Commands.Create;

public sealed record CreateArticleCommand(
    string Title,
    string Description,
    Guid SourceId)
    : ICommand<Result<CreateArticleResponse>>;