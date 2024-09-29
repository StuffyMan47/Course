using Course.Application.Services.UserContext.Models;

namespace Course.Application.Services.UserContext;

public interface IUserContextProvider
{
    UserContextModel GetUserContext();
}