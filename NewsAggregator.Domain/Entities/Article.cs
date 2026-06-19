using NewsAggregator.Domain.Common;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Domain.Entities;

public sealed class Article
    : AuditableEntity,
      IBaseEntity,
      IHasDomainEvents
{
    private readonly List<ArticleCategory>
            _articleCategories = [];

    private readonly List<DomainEvent>
        _domainEvents = [];

    private Article()
    {
    }

    public Article(
        string title,
        string description,
        string url,
        DateTime publishedAt,
        Guid sourceId)
    {
        Id = Guid.NewGuid();

        Title = title;

        Description = description;

        Url = url;

        PublishedAt = publishedAt;

        SourceId = sourceId;

        _domainEvents.Add(
            new ArticleCreatedEvent(
                Id,
                Title,
                SourceId));
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public string Url { get; private set; } = null!;

    public DateTime PublishedAt { get; private set; }

    public Guid SourceId { get; private set; }

    public Source Source { get; private set; } = null!;

    public IReadOnlyCollection<ArticleCategory>
        ArticleCategories
            => _articleCategories.AsReadOnly();

    public IReadOnlyCollection<DomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    public void AddCategory(
        Category category)
    {
        if (_articleCategories.Any(
            x => x.CategoryId == category.Id))
        {
            return;
        }

        _articleCategories.Add(
            new ArticleCategory(
                Id,
                category.Id));
    }

    public void AddCategories(
        IEnumerable<Category> categories)
    {
        foreach (var category in categories)
        {
            AddCategory(category);
        }
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}