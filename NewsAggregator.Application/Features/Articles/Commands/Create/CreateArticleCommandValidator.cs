using FluentValidation;

namespace NewsAggregator.Application.Features.Articles.Commands.Create;

internal class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.SourceId)
            .NotEmpty();
    }
}
