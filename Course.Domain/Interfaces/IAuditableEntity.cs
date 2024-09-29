namespace Course.Domain.Interfaces;

public interface IAuditableEntity
{
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; }
    public Guid LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
}