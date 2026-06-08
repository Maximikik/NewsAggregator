namespace NewsAggregator.Domain.Entities;

public sealed class UserCategoryPreference
{
    private UserCategoryPreference()
    {
    }

    public UserCategoryPreference(
        Guid userId,
        Guid categoryId)
    {
        UserId = userId;
        CategoryId = categoryId;
        Weight = 1;
    }

    public Guid UserId { get; private set; }

    public Guid CategoryId { get; private set; }

    public double Weight { get; private set; }

    public User User { get; private set; } = null!;

    public Category Category { get; private set; } = null!;

    public void IncreaseWeight()
    {
        Weight++;
    }
}