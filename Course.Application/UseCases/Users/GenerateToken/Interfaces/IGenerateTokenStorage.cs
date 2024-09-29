using Course.Application.UseCases.Users.GenerateToken.Models;

namespace Course.Application.UseCases.Users.GenerateToken.Interfaces;

public interface IGenerateTokenStorage
{
    Task SaveUserRefreshToken(SaveUserTokenRequest request);

}