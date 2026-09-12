namespace NewsAggregator.Application.Common.Authentication;

public interface IRefreshTokenGenerator
{
    GeneratedRefreshToken Generate();
}
