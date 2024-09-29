using Course.Domain.Interfaces;

namespace Course.Domain.Entities.Base;

public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity
{
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public Guid LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; } = DateTimeOffset.UtcNow;
}