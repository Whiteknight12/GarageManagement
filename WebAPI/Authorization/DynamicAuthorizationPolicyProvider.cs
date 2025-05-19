using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebAPI.Authorization
{
    public class DynamicAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;
        private readonly ILogger<DynamicAuthorizationPolicyProvider> _logger;
        public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, ILogger<DynamicAuthorizationPolicyProvider> logger)
        {
            _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
            _logger = logger;
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return _fallbackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return _fallbackPolicyProvider.GetFallbackPolicyAsync();
        }   

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            _logger.LogInformation("Creating dynamic policy for: {PolicyName}", policyName);

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new DynamicPermissionRequirement(policyName))
                .Build();

            _logger.LogInformation("Policy created: {PolicyDetails}", policy.ToString());

            return Task.FromResult(policy);
        }
    }
}
