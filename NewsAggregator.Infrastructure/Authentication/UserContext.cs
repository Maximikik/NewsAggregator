using Microsoft.AspNetCore.Http;
using NewsAggregator.Application.Common.Authentication;
using System.Security.Claims;

namespace NewsAggregator.Infrastructure.Authentication;

public sealed class UserContext
    : IUserContext
{
    private readonly IHttpContextAccessor
        _httpContextAccessor;

    public UserContext(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor =
            httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var value =
                _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirst(
                        ClaimTypes.NameIdentifier);

            if (value is null)
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated.");
            }

            return Guid.Parse(value!.Value);
        }
    }

    public string Email
    {
        get
        {
            var value =
                _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirst(
                        ClaimTypes.Email);

            if (value is null)
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated.");
            }

            return value.Value;
        }
    }
}
