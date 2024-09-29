using Course.Application.Services.UserContext;
using Course.Application.UseCases.Permissions.GetUserPermissions.Models;

namespace Course.Application.UseCases.Permissions.GetUserPermissions.Interfaces;

public interface IGetUserPermissionsStorage : IScopedService
{
    Task<List<GetUserPermissionResponse>> GetUserPermissions(Guid userId);
}
