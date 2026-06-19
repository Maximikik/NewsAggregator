using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Features.Categories.Commands.Create;

internal sealed class CreateCategoryHandler(
    INewsAggregatorDbContext _context)
    : IRequestHandler<
        CreateCategoryCommand, Result<CreateCategoryResponse>>
{
    public async ValueTask<Result<CreateCategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists =
            await _context.Categories
                .AnyAsync(
                    x => x.Name == command.Name,
                    cancellationToken);

        if (exists)
        {
            return Result<CreateCategoryResponse>
                .Failure(
                    Errors.Conflict(
                        "Category already exists"));
        }

        var category =
            new Category(command.Name);

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(
            cancellationToken);

        return Result<CreateCategoryResponse>
            .Success(
                new CreateCategoryResponse(
                    category.Id));
    }
}