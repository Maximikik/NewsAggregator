using FluentValidation;

namespace NewsAggregator.Application.Features.Categories.Commands.Create;

internal class CreateCategoryCommandValidator
    : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).Length(1, 50);
    }
}