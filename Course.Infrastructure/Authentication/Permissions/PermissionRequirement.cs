using Microsoft.AspNetCore.Authorization;

namespace Course.Infrastructure.Authentication.Permissions;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}