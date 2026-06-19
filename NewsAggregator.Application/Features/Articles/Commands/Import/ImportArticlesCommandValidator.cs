using FluentValidation;

namespace NewsAggregator.Application.Features.Articles.Commands.Import;

internal class ImportArticlesCommandValidator
    : AbstractValidator<ImportArticlesCommand>
{
    public ImportArticlesCommandValidator()
    {
        RuleFor(x => x.SourceId).NotEmpty();
    }
}
