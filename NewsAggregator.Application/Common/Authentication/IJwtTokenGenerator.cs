using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Common.Authentication;

public interface IJwtTokenGenerator
{
    GeneratedToken Generate(User user);
}
