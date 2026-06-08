namespace NewsAggregator.Domain.Entities;

public sealed class Category
{
    private readonly List<ArticleCategory>
        _articleCategories = [];

    private readonly List<UserCategoryPreference>
        _preferences = [];

    private Category()
    {
    }

    public Category(
        string name)
    {
        Id = Guid.NewGuid();

        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public IReadOnlyCollection<ArticleCategory>
        ArticleCategories =>
            _articleCategories.AsReadOnly();

    public IReadOnlyCollection<UserCategoryPreference>
        Preferences =>
            _preferences.AsReadOnly();
}