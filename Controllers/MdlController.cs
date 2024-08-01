using AuthSample.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MdlController(IMdlBaseService mdlService) : Controller
{
    [HttpGet("list")]
    public IActionResult List()
    {
        mdlService.List();

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        mdlService.Get(id);

        return Ok();
    }

    [HttpPost]
    public IActionResult Create()
    {
        mdlService.Add();

        return Ok();
    }

    [HttpPut]
    public IActionResult Update()
    {
        mdlService.Update();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete()
    {
        mdlService.Delete();

        return Ok();
    }

    [HttpPut("Barcode")]
    [ApiPermission("Base", "Mdl", "AddBarcode")]
    public IActionResult AddBarcode() { return Ok(); }
}
