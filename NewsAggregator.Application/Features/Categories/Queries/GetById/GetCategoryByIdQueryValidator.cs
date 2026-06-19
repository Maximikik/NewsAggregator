using FluentValidation;

namespace NewsAggregator.Application.Features.Categories.Queries.GetById;

internal class GetCategoryByIdQueryValidator
    : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}