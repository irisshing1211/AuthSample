using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthSample;
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class ApiPermissionAttribute(string module, string func, string action) : Attribute, IAuthorizationFilter
{
    public string RequiredPermission { get; } = $"{module}_{func}_{action}";
    public string Module { get; } = module;
    public string Func { get; } = func;
    public string Action { get; } = action;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userPermissions = context.HttpContext.User.FindFirst("Permissions")?.Value;
        if (userPermissions == null || !userPermissions.Contains(RequiredPermission))
        {
            context.Result = new ForbidResult();
        }
    }
}
