using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

public class RolePermissionFilter : IAuthorizationFilter
{
    private readonly string _permission;
    private readonly DataContext _context;

    public RolePermissionFilter(string permission, DataContext context)
    {
        _permission = permission;
        _context = context;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var roleId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "0");

        var hasPermission = _context.RolePermissions
            .Any(rp => rp.RoleId == roleId && rp.Permission.Name == _permission);

        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}
