using Course.Domain.Entities.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Course.Infrastructure.Authentication.Attributes;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = CoursePermission.NameFor(action, resource);
}