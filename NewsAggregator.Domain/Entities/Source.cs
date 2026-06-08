using NewsAggregator.Domain.Common;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Domain.Entities;

public sealed class Source :
    AuditableEntity,
    IBaseEntity,
    IHasDomainEvents
{
    private readonly List<Article> _articles = [];

    private readonly List<Feed> _feeds = [];

    private readonly List<DomainEvent> _domainEvents = [];

    private Source()
    {
    }

    public Source(
        string name,
        string baseUrl)
    {
        Id = Guid.NewGuid();

        Name = name;

        BaseUrl = baseUrl;

        IsActive = true;

        _domainEvents.Add(
            new SourceCreatedEvent(Id, Name));
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string BaseUrl { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Article> Articles
        => _articles.AsReadOnly();

    public IReadOnlyCollection<Feed> Feeds
        => _feeds.AsReadOnly();

    public IReadOnlyCollection<DomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Disable()
    {
        IsActive = false;
    }
}