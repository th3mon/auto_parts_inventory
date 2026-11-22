using Catalog.Api.Models;

namespace Catalog.Api.Services;

public class PartsService : IPartsService
{
    private readonly List<Part> Parts = new()
    {
        new Part { Id = Guid.NewGuid(), Name = "Part 1", Price = 66.6m, Sku = "Part 1.1", Manufacturer = "Man666", StockQuantity = 666 },
        new Part { Id = Guid.NewGuid(), Name = "Part 2", Price = 6.66m, Sku = "Part 2.1", Manufacturer = "Man666", StockQuantity = 333 }
    };

    public IReadOnlyList<Part> GetAll()
    {
        return Parts;
    }

    public Part? GetById(Guid id)
    {
        return GetAll().FirstOrDefault(p => p.Id == id);
    }

    public void Add(Part part)
    {
        Parts.Add(part);
    }

    public bool Update(Guid id, Part updatedPart)
    {
        var part = GetById(id);

        if (part == null)
        {
            return false;
        }

        part.Name = updatedPart.Name;
        part.Price = updatedPart.Price;
        part.Sku = updatedPart.Sku;
        part.Manufacturer = updatedPart.Manufacturer;
        part.StockQuantity = updatedPart.StockQuantity;

        return true;
    }

    public bool Remove(Guid id)
    {
        var part = GetById(id);

        if (part == null)
        {
            return false;
        }

        return Parts.Remove(part);
    }
}
