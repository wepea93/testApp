using System.ComponentModel.DataAnnotations;

namespace InventoryService.Core;

public class Inventory
{
    
    [Required]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "la cantidad debe ser mayor o igual a 0")]
    public int Quantity{ get; set; }
}
