using FluentValidation;

namespace NewsAggregator.Application.Features.Articles.Queries.GetAll;

internal class GetAllArticlesQueryValidator
    : AbstractValidator<GetAllArticlesQuery>
{
    public GetAllArticlesQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).LessThan(int.MaxValue);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThan(int.MaxValue);
    }
}
