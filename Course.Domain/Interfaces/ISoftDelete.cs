namespace Course.Domain.Interfaces;

public interface ISoftDelete
{
    DateTimeOffset? DeletedAt { get; set; }
    Guid? DeletedBy { get; set; }
}