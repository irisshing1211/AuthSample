using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UserInfoController : Controller
{
    [HttpGet]
   // [ApiPermission("Sys", "UserInfo", "Read")]
    public IActionResult Get() { return Ok(); }
}
