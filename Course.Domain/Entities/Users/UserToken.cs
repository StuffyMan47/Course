using Course.Domain.Entities.Base;

namespace Course.Domain.Entities.Users;

public class UserToken : BaseEntity<long>
{
    public required string RefreshToken { get; set; }
    public required Guid UserId { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
}