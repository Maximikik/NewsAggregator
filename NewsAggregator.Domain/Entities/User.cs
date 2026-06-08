namespace NewsAggregator.Domain.Entities;

public sealed class User
{
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
}
