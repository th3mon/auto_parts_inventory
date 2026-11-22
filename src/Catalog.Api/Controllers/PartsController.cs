using Catalog.Api.Dtos;
using Catalog.Api.Models;
using Catalog.Api.Services;
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

    private readonly PartsService partsService;

    public PartsController(PartsService partsService)
    {
        this.partsService = partsService;
    }

    [HttpGet]
    public ActionResult<List<Part>> GetParts()
    {
        return Ok(Parts);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Part> GetPart(Guid id)
    {
        var part = Parts.FirstOrDefault(p => p.Id == id);

        if (part != null)
        {
            return Ok(part);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<Part> CreatePart(CreatePartRequest request)
    {
        var part = new Part()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Sku = request.Sku,
            Manufacturer = request.Manufacturer,
            StockQuantity = request.StockQuantity
        };

        Parts.Add(part);

        return CreatedAtAction(
            nameof(GetPart),
            new { id = part.Id },
            part
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdatePart(Guid id, UpdatePartRequest request)
    {
        var part = Parts.FirstOrDefault(p => p.Id == id);

        if (part == null)
        {
            return NotFound();
        }

        part.Name = request.Name;
        part.Price = request.Price;
        part.Sku = request.Sku;
        part.Manufacturer = request.Manufacturer;
        part.StockQuantity = request.StockQuantity;

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePart(Guid id)
    {
        var part = Parts.FirstOrDefault(p => p.Id == id);

        if (part == null)
        {
            return NotFound();
        }

        Parts.Remove(part);

        return NoContent();
    }
}

