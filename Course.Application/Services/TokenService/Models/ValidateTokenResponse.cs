using Course.Domain.Entities.Users;

namespace Course.Application.Services.TokenService.Models;

public class ValidateTokenResponse
{
    public required bool IsSuccesss { get; init; }
    public required Guid UserId { get; init; }
    public required UserRole UserRole { get; init; }
    public required string Login { get; init; }
}