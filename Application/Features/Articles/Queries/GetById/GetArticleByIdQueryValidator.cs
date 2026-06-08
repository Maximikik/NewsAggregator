using FluentValidation;

namespace NewsAggregator.Application.Features.Articles.Queries.GetArticleById;

public class GetArticleByIdQueryValidator : AbstractValidator<GetArticleByIdQuery>
{
    public GetArticleByIdQueryValidator()
    {
        RuleFor(item => item.Id).NotEmpty();
    }
}
