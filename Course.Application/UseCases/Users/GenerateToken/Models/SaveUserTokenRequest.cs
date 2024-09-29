namespace Course.Application.UseCases.Users.GenerateToken.Models;

public class SaveUserTokenRequest
{
    public required Guid UserId { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTimeOffset TokenExpirationDate { get; init; }
}