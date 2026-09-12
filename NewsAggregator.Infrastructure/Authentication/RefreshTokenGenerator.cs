using Microsoft.Extensions.Options;
using NewsAggregator.Application.Common.Authentication;
using System.Security.Cryptography;

namespace NewsAggregator.Infrastructure.Authentication;

internal sealed class RefreshTokenGenerator(
    IOptions<JwtOptions> options)
    : IRefreshTokenGenerator
{
    private readonly JwtOptions _options = options.Value;

    public GeneratedRefreshToken Generate()
    {
        var value = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var expiresAt = DateTime.UtcNow.AddDays(_options.RefreshTokenExpirationDays);

        return new GeneratedRefreshToken(value, expiresAt);
    }
}
