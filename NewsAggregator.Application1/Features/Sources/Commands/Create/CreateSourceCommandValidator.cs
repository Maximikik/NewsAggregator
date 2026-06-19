using FluentValidation;
using NewsAggregator.Application.Features.Sources.Create;

namespace NewsAggregator.Application.Features.Sources.Commands.Create;

internal class CreateSourceCommandValidator
    : AbstractValidator<CreateSourceCommand>
{
    public CreateSourceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.BaseUrl)
                    .NotEmpty()
                    .MaximumLength(500)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var result)
                                  && (result.Scheme == Uri.UriSchemeHttp
                                      || result.Scheme == Uri.UriSchemeHttps))
                    .WithMessage("BaseUrl must be a valid HTTP or HTTPS URL.");
    }
}
