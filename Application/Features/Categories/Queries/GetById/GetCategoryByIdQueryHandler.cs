using Mediator;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Results;
using NewsAggregator.Application.Features.Categories.Shared;

namespace NewsAggregator.Application.Features.Categories.Queries.GetById;

internal class GetCategoryByIdQueryHandler(
    INewsAggregatorDbContext _context)
    : IQueryHandler<GetCategoryByIdQuery, Result<CategoryResponse>>
{
    public async ValueTask<Result<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category =
                  await _context.Categories
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
                CategoryMapper.ToResponse(category)
                );
    }
}