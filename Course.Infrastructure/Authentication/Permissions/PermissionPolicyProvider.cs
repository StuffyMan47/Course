using Course.Application.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Course.Infrastructure.Authentication.Permissions;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
{
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; } = new(options);

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(BaseConstants.PermissionClaimKey, StringComparison.OrdinalIgnoreCase))
            return FallbackPolicyProvider.GetPolicyAsync(policyName);

        var policy = new AuthorizationPolicyBuilder();
        _ = policy.AddRequirements(new PermissionRequirement(policyName));
        return Task.FromResult<AuthorizationPolicy?>(policy.Build());

    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);
}
