using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authorization
{
    public class DynamicPermissionHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicPermissionRequirement requirement)
        {
            var hasPermission = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission);

            if (hasPermission) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
