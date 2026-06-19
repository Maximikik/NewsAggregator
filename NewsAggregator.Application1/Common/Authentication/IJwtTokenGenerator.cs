using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Common.Authentication;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
