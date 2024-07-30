namespace AuthSample;

public class PermissionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var requiredPermissions = endpoint.Metadata.OfType<ApiPermissionAttribute>().Select(attr => attr.RequiredPermission).ToList();

            if (requiredPermissions.Any())
            {
                var userPermissions = context.User.FindFirst("Permissions")?.Value;

                if (userPermissions == null || !requiredPermissions.All(p => userPermissions.Contains(p)))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: Insufficient permissions");
                    return;
                }
            }
        }

        await next(context);
    }
}