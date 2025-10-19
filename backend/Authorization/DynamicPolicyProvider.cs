using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

public class DynamicPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public DynamicPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

    public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(new FeatureRequirement(policyName));
        return Task.FromResult<AuthorizationPolicy?>(policy.Build());
    }
}
