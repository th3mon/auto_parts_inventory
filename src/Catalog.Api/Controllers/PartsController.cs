using Catalog.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/parts")]
public class PartsController : ControllerBase
{
    private static readonly List<Part> Parts = new()
    {
        new Part { Id = Guid.NewGuid(), Name = "Part 1", Price = 66.6m, Sku = "Part 1.1", Manufacturer = "Man666", StockQuantity = 666 },
        new Part { Id = Guid.NewGuid(), Name = "Part 2", Price = 6.66m, Sku = "Part 2.1", Manufacturer = "Man666", StockQuantity = 333 }
    };

    [HttpGet]
    public ActionResult<List<Part>> GetParts()
    {
        return Ok(PartsController.Parts);
    }
}
