namespace NewsAggregator.Domain.Entities;

public interface IBaseEntity
{
    public Guid Id { get; }
}

public abstract class AuditableEntity // TODO: update BL
{
    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
}