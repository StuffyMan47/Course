using Course.Domain.Entities.Base;

namespace Course.Domain.Entities.Users;

public class User : BaseEntity<Guid>
{
    public required string Username { get; set; }
    public required string Login { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    public string? ImageUrl { get; set; }
    
    public UserRole UserRole { get; set; }
    public UserStatus UserStatus { get; set; }
    
    public List<UserToken> UserTokens { get; set; } = [];

}