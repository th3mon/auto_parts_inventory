namespace Catalog.Api.Dtos;

public class CreatePartRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
}
