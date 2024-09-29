using Course.Domain.Entities.Users;

namespace Course.Application.UseCases.Users.Login.Models;

public class UserModel
{
    public required Guid UserId { get; init; }
    public required UserRole UserRole { get; init; }
    public required string Login { get; init; }
    public required string PasswordHash { get; init; }
    public required string PasswordSalt { get; init; }
}