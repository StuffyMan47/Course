using Course.Application.UseCases.Users.RefreshToken.Models;

namespace Course.Application.UseCases.Users.RefreshToken.Interfaces;

public interface IRefreshTokenStorage
{
    Task<UserModel?> GetUser(Guid userId, string refreshToken);
}