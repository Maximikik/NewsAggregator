using NewsAggregator.Application.Common.Authentication;
using System.Security.Cryptography;

namespace NewsAggregator.Infrastructure.Authentication;

internal sealed class RefreshTokenGenerator
    : IRefreshTokenGenerator
{
    public string Generate()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator
                .GetBytes(64));
    }
}
