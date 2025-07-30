using System.ComponentModel.DataAnnotations;

namespace ProductService.Core;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
    public decimal Price{ get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}
