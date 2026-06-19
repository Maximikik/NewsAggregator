namespace NewsAggregator.Domain.Entities;

public sealed class User
{
    private readonly List<UserArticleLike> _likes = [];

    private readonly List<UserCategoryPreference> _preferences = [];

    private readonly List<RefreshToken> _refreshTokens = [];

    private User()
    {
    }

    public User(
        string email,
        string passwordHash)
    {
        Id = Guid.NewGuid();

        Email = email;

        PasswordHash = passwordHash;
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public IReadOnlyCollection<UserArticleLike>
        Likes =>
            _likes.AsReadOnly();

    public IReadOnlyCollection<UserCategoryPreference>
        Preferences =>
            _preferences.AsReadOnly();

    public IReadOnlyCollection<RefreshToken>
        RefreshTokens =>
            _refreshTokens.AsReadOnly();
}
