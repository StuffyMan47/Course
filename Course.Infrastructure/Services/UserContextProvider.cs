using Course.Application.Constants;
using Course.Application.Services.UserContext;
using Course.Application.Services.UserContext.Models;
using Course.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;

namespace Course.Infrastructure.Services;

public class UserContextProvider(IHttpContextAccessor httpContextAccessor) : IUserContextProvider
{
    public UserContextModel GetUserContext()
    {
        var user = GetUser(httpContextAccessor.HttpContext);
        if (user.Id != Guid.Empty)
            return user;

        return new();
    }

    private UserContextModel GetUser(HttpContext? context)
    {
        if (context == null)
            return new();

        var user = context.User;
        try
        {
            var userId = Guid.Parse(user.Claims.First(x => x.Type == TokenClaimKeys.UserId).Value);
            var systemRole = Enum.Parse<UserRole>(user.Claims.First(x => x.Type == TokenClaimKeys.UserRole).Value);
            string login = user.Claims.First(x => x.Type == TokenClaimKeys.Login).Value;

            return new()
            {
                Id = userId,
                Login = login,
                UserRole = systemRole,
            };
        }
        catch
        {
            return new();
        }
    }
}