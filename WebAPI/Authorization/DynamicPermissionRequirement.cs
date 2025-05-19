using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authorization
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public DynamicPermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}   
