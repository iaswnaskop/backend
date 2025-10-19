using Microsoft.AspNetCore.Authorization;

public class FeatureRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public FeatureRequirement(string permission)
    {
        Permission = permission;
    }
}
