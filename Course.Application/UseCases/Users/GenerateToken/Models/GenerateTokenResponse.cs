namespace Course.Application.UseCases.Users.GenerateToken.Models;

public class GenerateTokenResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTimeOffset RefreshExpirationDate { get; init; }
}