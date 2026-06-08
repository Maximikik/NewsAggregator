using NewsAggregator.Domain.Common;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Domain.Entities;

public sealed class Category :
    AuditableEntity,
    IBaseEntity,
    IHasDomainEvents
{
    private readonly List<Article> _articles = [];

    private readonly List<DomainEvent> _domainEvents = [];

    private Category()
    {
    }

    public Category(string name)
    {
        Id = Guid.NewGuid();

        Name = name;

        _domainEvents.Add(
            new CategoryCreatedEvent(Id, Name));
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public IReadOnlyCollection<Article> Articles
        => _articles.AsReadOnly();

    public IReadOnlyCollection<DomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}