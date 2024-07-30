using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    [HttpGet("list")]
    [ApiPermission("Setting", "User", "List")]
    public IActionResult List() { return Ok(); }

    [HttpGet("{id}")]
    [ApiPermission("Setting", "User", "Read")]
    public IActionResult Get(int id) { return Ok(); }

    [HttpPost]
    [ApiPermission("Setting", "User", "Add")]
    public IActionResult Create() { return Ok(); }

    [HttpPut]
    [ApiPermission("Setting", "User", "Update")]
    public IActionResult Update() { return Ok(); }

    [HttpDelete("{id}")]
    [ApiPermission("Setting", "User", "Delete")]
    public IActionResult Delete() { return Ok(); }

}
