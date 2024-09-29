namespace Course.Application.UseCases.Users.Login.Models;

public record LoginResponse(string AccessToken, string RefreshToken);
