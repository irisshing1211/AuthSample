using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserModel user)
    {
        // 註冊邏輯
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // 登入邏輯
        return Ok();
    }
}

public class UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

