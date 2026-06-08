using NewsAggregator.Domain.Common;
using NewsAggregator.Domain.Events;

namespace NewsAggregator.Domain.Entities;

public sealed class UserArticleLike
    : IHasDomainEvents
{
    private readonly List<DomainEvent>
        _domainEvents = [];

    private UserArticleLike()
    {
    }

    public UserArticleLike(
        Guid userId,
        Guid articleId)
    {
        Id = Guid.NewGuid();

        UserId = userId;

        ArticleId = articleId;

        CreatedAtUtc = DateTime.UtcNow;

        _domainEvents.Add(
            new ArticleLikedEvent(
                userId,
                articleId));
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public Guid ArticleId { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public User User { get; private set; } = null!;

    public Article Article { get; private set; } = null!;

    public IReadOnlyCollection<DomainEvent>
        DomainEvents =>
            _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}