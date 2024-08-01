namespace AuthSample.Services;

// 权限检查接口
public interface IPermissionService
{
    bool HasPermission(string permissionCode);
}
// 权限检查实现
public class PermissionService(IHttpContextAccessor httpContextAccessor) : IPermissionService
{
    public bool HasPermission(string permissionCode)
    {
        var userPermissions = httpContextAccessor.HttpContext?.User.FindFirst("Permissions")?.Value;
        return userPermissions != null && userPermissions.Contains(permissionCode);
    }
}
