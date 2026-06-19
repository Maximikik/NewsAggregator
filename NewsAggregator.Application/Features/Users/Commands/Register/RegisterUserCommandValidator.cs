using FluentValidation;

namespace NewsAggregator.Application.Features.Users.Commands.Register;

internal class RegisterUserCommandValidator
    : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email).Length(10, 230);
        RuleFor(x => x.Password).Length(10, 230);
    }
}
