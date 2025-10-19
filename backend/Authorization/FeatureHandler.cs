using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class FeatureHandler : AuthorizationHandler<FeatureRequirement, dynamic>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        FeatureRequirement requirement,
        dynamic resource)
    {
        var permissions = context.User.FindAll("Permission").Select(c => c.Value).ToList();

        var maxLanguages = int.TryParse(context.User.FindFirst("MaxLanguages")?.Value, out var ml) ? ml : int.MaxValue;
        var maxLayouts = int.TryParse(context.User.FindFirst("MaxLayouts")?.Value, out var mlayout) ? mlayout : int.MaxValue;
        var maxStores = int.TryParse(context.User.FindFirst("MaxStores")?.Value, out var ms) ? ms : int.MaxValue;

        switch (requirement.Permission)
        {
            case "AddLanguage":
                if (permissions.Contains("AddLanguage") && resource.CurrentLanguages < maxLanguages)
                    context.Succeed(requirement);
                break;

            case "AddLayout":
                if (permissions.Contains("AddLayout") && resource.CurrentLayouts < maxLayouts)
                    context.Succeed(requirement);
                break;

            case "AddStore":
                if (permissions.Contains("AddStore") && resource.CurrentStores < maxStores)
                    context.Succeed(requirement);
                break;

            case "DeleteVendor":
                if (permissions.Contains("DeleteVendor") &&
                    (context.User.IsInRole("Admin") ||
                     resource.OwnerId == context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                {
                    context.Succeed(requirement);
                }
                break;
        }

        return Task.CompletedTask;
    }
}
