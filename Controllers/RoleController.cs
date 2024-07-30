using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoleController : Controller
{
    [HttpGet("list")]
    [ApiPermission("Setting", "Role", "List")]
    public IActionResult List() { return Ok(); }

    [HttpGet("{id}")]
    [ApiPermission("Setting", "Role", "Read")]
    public IActionResult Get(int id) { return Ok(); }

    [HttpPost]
    [ApiPermission("Setting", "Role", "Add")]
    public IActionResult Create() { return Ok(); }

    [HttpPut]
    [ApiPermission("Setting", "Role", "Update")]
    public IActionResult Update() { return Ok(); }

    [HttpDelete("{id}")]
    [ApiPermission("Setting", "Role", "Delete")]
    public IActionResult Delete() { return Ok(); }

}
