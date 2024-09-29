using Course.Domain.Entities.Users;

namespace Course.Application.UseCases.Users.RefreshToken.Models;

public class UserModel
{
    public required int DefaultOwnerId { get; init; }
    public required Guid UserId { get; init; }
    public required UserRole UserRole { get; init; }
    public required string Login { get; init; }
    public required int TenantId { get; init; }
    public required string? MarkUserId { get; init; }
    public required string? RefreshTokenFromDb { get; init; }
    public required DateTimeOffset? RefreshTokenExpires { get; init; }
    public required List<AvaliableEmployee> AvaliableEmployees { get; init; }
}

public record AvaliableEmployee(int OwnerId, int EmployeeId);