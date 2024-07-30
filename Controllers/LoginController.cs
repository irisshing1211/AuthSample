using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthSample.Controllers;

[ApiController, AllowAnonymous]
[Route("api/[controller]")]
public class LoginController(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    ApplicationDbContext ctx) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Username);

        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password)) { return Unauthorized(); }

        var userRoles = await userManager.GetRolesAsync(user);

        var rolePermissions = ctx.RolePermissions.Where(rp => userRoles.Contains(rp.Role.Name))
                                 .Select(rp => rp.Permission.Code)
                                 .Distinct()
                                 .ToList();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName), new Claim("UserId", user.Id.ToString())
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(rolePermissions.Select(permission => new Claim("Permission", permission)));
        var token = GenerateJwtToken(user, claims);

        return Ok(new { Token = token });
    }

    string GenerateJwtToken(ApplicationUser user, IList<Claim> claims)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"],
                                         audience: configuration["Jwt:Audience"],
                                         claims: claims,
                                         expires: DateTime.Now.AddMinutes(
                                             Convert.ToDouble(configuration["Jwt:ExpiryInMinutes"])),
                                         signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public struct LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
