namespace Course.Application.UseCases.Permissions.GetUserPermissions.Models;

public enum PermissionSource
{
    Claims,
    Role
}

public record GetUserPermissionResponse(string Permission, PermissionSource Source);
