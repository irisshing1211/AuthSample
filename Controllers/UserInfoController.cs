using System.Security.Claims;
using AuthSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthSample.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UserInfoController(ApplicationDbContext ctx) : Controller
{
    [HttpGet]

    // [ApiPermission("Sys", "UserInfo", "Read")]
    public IActionResult Get()
    {
        //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // 獲取用戶的角色
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        // 獲取用戶的其他聲明
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var permissions = GetPermission();

        // 返回用戶資訊
        return Ok(new UserInfoResponse { Name = name, Email = email, Roles = roles, Menu = permissions });
    }

    List<UserMenuModel> GetPermission()
    {
        var userPermission = User.FindAll("Permission").Select(a => a.Value).ToList();

        var permissions =
            (from u in userPermission join p in ctx.Permissions.AsNoTracking() on u equals p.Code select p).ToList();

        var modules = permissions.Select(a => a.Module)
                                 .Distinct()
                                 .Select(a => new UserMenuModel() { Name = a })
                                 .ToList();

        foreach (var m in modules)
        {
            var func = permissions.Where(a => a.Module == m.Name).ToList();
                var funcList=func
                                  .Select(a => a.Func)
                                  .Distinct()
                                  .Select(a => new UserMenuModel() { Name = a })
                                  .ToList();

            foreach (var f in funcList)
            {
                var actions = func.Where(a => a.Func == f.Name)
                                  .Select(a => new UserMenuModel() { Name = a.Action, Code = a.Code })
                                  .ToList();

                f.Children = actions;
            }

            m.Children = funcList;
        }

        return modules;
    }
}

record UserInfoResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public List<UserMenuModel> Menu { get; set; }
}

record UserMenuModel
{
    public string Name { get; set; }
    public string Code { get; set; }
    public List<UserMenuModel> Children { get; set; }
}
