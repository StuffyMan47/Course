using Course.Application.UseCases.Permissions.GetUserPermissions;
using Microsoft.AspNetCore.Authorization;

namespace Course.Infrastructure.Authentication.Permissions;

public class PermissionHandler(GetUserPermissionsUseCase useCase) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = await useCase.GetCurrentUserPermissions();

        if (permissions.IsSuccess && permissions.Value.Select(x => x.Permission).Contains(requirement.Permission))
            context.Succeed(requirement);
    }
}