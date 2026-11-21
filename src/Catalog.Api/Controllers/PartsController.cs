using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/parts")]
public class PartsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetParts()
    {
        return Ok("Hello from Catalog API");
    }
}
