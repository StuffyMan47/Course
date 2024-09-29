using Course.Domain.Entities.Users;

namespace Course.Application.UseCases.Users.GenerateToken.Models;

public class GenerateTokenRequest
{
    public required Guid UserId { get; init; }
    public required UserRole UserRole { get; init; }
    public required string Login { get; init; }
}