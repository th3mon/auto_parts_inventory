using Catalog.Api.Models;

namespace Catalog.Api.Services;

public interface IPartsService
{
    IReadOnlyList<Part> GetAll();
    Part? GetById(Guid id);
    void Add(Part part);
    bool Update(Guid id, Part updatedPart);
    bool Remove(Guid id);
}
