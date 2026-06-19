using Mediator;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Categories.Commands.Create;

public sealed record CreateCategoryCommand(
    string Name)
    : ICommand<Result<CreateCategoryResponse>>;