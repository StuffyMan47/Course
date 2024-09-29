using Course.Application.UseCases.Users.Login.Models;

namespace Course.Application.UseCases.Users.Login.Interfaces;

public interface ILoginStorage
{
    Task<UserModel?> GetUser(string login);
}