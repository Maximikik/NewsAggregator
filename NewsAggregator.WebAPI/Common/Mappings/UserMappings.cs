using NewsAggregator.Application.Features.Users.Commands.Login;
using NewsAggregator.Application.Features.Users.Commands.Register;
using NewsAggregator.WebAPI.Contracts.Users;

namespace NewsAggregator.WebAPI.Common.Mappings;

internal static class UserMappings
{
    internal static RegisterUserCommand ToRegisterCommand(
        this RegisterUserRequest request)
    {
        return new RegisterUserCommand(
            request.Email,
            request.Password);
    }

    internal static LoginUserCommand ToLoginCommand(
        this LoginUserRequest request)
    {
        return new LoginUserCommand(
            request.Email,
            request.Password);
    }
}
