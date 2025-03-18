using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RolePermissionAttribute : TypeFilterAttribute
{
    public RolePermissionAttribute(string permission) : base(typeof(RolePermissionFilter))
    {
        Arguments = new object[] { permission };
    }
}
