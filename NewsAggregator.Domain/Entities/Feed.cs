namespace NewsAggregator.Domain.Entities;

public sealed class Feed :
    AuditableEntity,
    IBaseEntity
{
    private Feed() { }

    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    public IEnumerable<Article>? Articles { get; set; }
}
