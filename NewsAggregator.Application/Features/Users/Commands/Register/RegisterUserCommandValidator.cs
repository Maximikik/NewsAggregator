using FluentValidation;

namespace NewsAggregator.Application.Features.Users.Commands.Register;

internal class RegisterUserCommandValidator
    : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(230);
        RuleFor(x => x.Password).NotEmpty().Length(10, 230);
    }
}
