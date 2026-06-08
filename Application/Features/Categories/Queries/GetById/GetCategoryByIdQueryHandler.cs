using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.Application.Features.Categories.Queries.GetById;

internal class GetCategoryByIdQueryHandler(
    INewsAggregatorDbContext context)
    : IRequestHandler<GetCategoryByIdQuery, Result<CategoryResponse>>
{
    public async ValueTask<Result<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category =
                  await context.Categories
                      .FirstOrDefaultAsync(
                          x => x.Id == query.Id,
                          cancellationToken);

        if (category is null)
        {
            return Result<CategoryResponse>
                .Failure(
                    Errors.NotFound("Category"));
        }

        return Result<CategoryResponse>
            .Success(
                new CategoryResponse(
                    category.Id,
                    category.Name));
    }
}