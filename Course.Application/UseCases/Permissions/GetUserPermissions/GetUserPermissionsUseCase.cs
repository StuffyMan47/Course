using Course.Application.ActionResult;
using Course.Application.Services.UserContext;
using Course.Application.UseCases.Permissions.GetUserPermissions.Interfaces;
using Course.Application.UseCases.Permissions.GetUserPermissions.Models;

namespace Course.Application.UseCases.Permissions.GetUserPermissions;

public class GetUserPermissionsUseCase(IGetUserPermissionsStorage storage, IUserContextProvider userProvider)
{
    public async Task<Result<List<GetUserPermissionResponse>>> GetCurrentUserPermissions()
    {
        var userContext = userProvider.GetUserContext();
        var result = await storage.GetUserPermissions(userContext.Id);
        return Result<List<GetUserPermissionResponse>>.Success(result);
    }

    public async Task<Result<List<GetUserPermissionResponse>>> GetUserPermissionsByUserId(Guid userId)
    {
        var result = await storage.GetUserPermissions(userId);
        return Result<List<GetUserPermissionResponse>>.Success(result);
    }
}
