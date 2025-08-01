namespace InventoryService.Api.DTO;

public class InventoryWithProductDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public ProductDto Product { get; set; }
}