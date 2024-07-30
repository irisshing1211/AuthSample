using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MdlController : Controller
{
    [HttpGet("list")]
    [ApiPermission("Base", "Mdl", "List")]
    public IActionResult List() { return Ok(); }

    [HttpGet("{id}")]
    [ApiPermission("Base", "Mdl", "Read")]
    public IActionResult Get(int id) { return Ok(); }

    [HttpPost]
    [ApiPermission("Base", "Mdl", "Add")]
    public IActionResult Create() { return Ok(); }

    [HttpPut]
    [ApiPermission("Base", "Mdl", "Update")]
    public IActionResult Update() { return Ok(); }

    [HttpDelete("{id}")]
    [ApiPermission("Base", "Mdl", "Delete")]
    public IActionResult Delete() { return Ok(); }

    [HttpPut("Barcode")]
    [ApiPermission("Base", "Mdl", "AddBarcode")]
    public IActionResult AddBarcode() { return Ok(); }
}
