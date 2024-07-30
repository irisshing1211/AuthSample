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
public class RegisterController(UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserModel model)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email, // Assuming email is the username
            Name = model.Name
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // add role
            return Ok(new { Message = "Registration successful" });
        }

        foreach (var error in result.Errors) { ModelState.AddModelError(string.Empty, error.Description); }

        return BadRequest(ModelState);
    }
}

public struct UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}
