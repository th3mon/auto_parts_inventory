namespace Catalog.Api.Models;

public class Part
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
}
