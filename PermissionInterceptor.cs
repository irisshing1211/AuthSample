using Castle.DynamicProxy;
using System;
using System.Reflection;
using AuthSample.Services;

namespace AuthSample;
public class PermissionInterceptor(IPermissionService permissionService) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var method = invocation.Method;
        var permissionAttr = method.GetCustomAttribute<ApiPermissionAttribute>();

        if (permissionAttr != null)
        {
            var permissionCode = $"{permissionAttr.Module}_{permissionAttr.Func}_{permissionAttr.Action}";

            if (!permissionService.HasPermission(permissionCode))
            {
                throw new UnauthorizedAccessException("Insufficient permissions");
            }
        }

        invocation.Proceed();
    }
}
public static class ServiceCollectionExtensions
{
    public static void AddProxiedServices(this IServiceCollection services, ProxyGenerator proxyGenerator, IPermissionService permissionService)
    {
        var serviceDescriptors = services.Where(s => s.ServiceType.IsInterface).ToList();

        foreach (var descriptor in serviceDescriptors)
        {
            if (descriptor.ServiceType.Namespace != null && descriptor.ServiceType.Namespace.StartsWith("AuthSample.Interfaces"))
            {
                // 创建拦截器
                var interceptor = new PermissionInterceptor(permissionService);

                // 获取接口类型
                var serviceType = descriptor.ServiceType;
                
                // 创建代理
                var proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget(serviceType, interceptor);
                
                // 注册代理
                services.AddTransient(serviceType, provider => proxy);
            }
        }
    }
}