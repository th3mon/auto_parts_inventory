using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Dtos;

public class UpdatePartRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(
        typeof(decimal),
        "0.01",
        "999999999.99",
        ParseLimitsInInvariantCulture = true
    )]
    public decimal Price { get; set; }

    [Required]
    public string Sku { get; set; } = string.Empty;

    [Required]
    public string Manufacturer { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}
