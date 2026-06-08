namespace NewsAggregator.Application.Common.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
    string Email { get; }
}
