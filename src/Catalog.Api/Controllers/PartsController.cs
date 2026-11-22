using Catalog.Api.Dtos;
using Catalog.Api.Models;
using Catalog.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/parts")]
public class PartsController : ControllerBase
{

    private readonly IPartsService partsService;

    public PartsController(IPartsService partsService)
    {
        this.partsService = partsService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Part>> GetParts()
    {
        return Ok(partsService.GetAll());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Part> GetPart(Guid id)
    {
        var part = partsService.GetById(id);

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

        partsService.Add(part);

        return CreatedAtAction(
            nameof(GetPart),
            new { id = part.Id },
            part
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdatePart(Guid id, UpdatePartRequest request)
    {
        var updated = partsService.Update(id, new Part
        {

            Name = request.Name,
            Price = request.Price,
            Sku = request.Sku,
            Manufacturer = request.Manufacturer,
            StockQuantity = request.StockQuantity,
        });

        if (updated)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePart(Guid id)
    {
        var deleted = partsService.Remove(id);

        if (deleted)
        {
            return NoContent();
        }


        return NotFound();
    }
}

