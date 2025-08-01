using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Core;

public class Inventory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "la cantidad debe ser mayor o igual a 0")]
    public int Quantity{ get; set; }
}
