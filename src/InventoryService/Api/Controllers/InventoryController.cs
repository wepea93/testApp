using Microsoft.AspNetCore.Mvc;
using InventoryService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using MassTransit;
using InventoryService.Core.Contracts;
using InventoryService.Api.DTO;


namespace InventoryService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly InventoryRepository _repository;
    private readonly IRequestClient<IGetProductById> _productRequestClient;

    public InventoryController(InventoryRepository repository,
        IRequestClient<IGetProductById> productRequestClient)
    {
        _repository = repository;
        _productRequestClient = productRequestClient;
    }

    /// <summary>
    /// Obtiene un inventario por su producto id.
    /// </summary>
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<Core.Inventory>> GetByProductId(int productId)
    {
        var Inventory = await _repository.GetByProductIdAsync(productId);
        if (Inventory == null) return NotFound();
        return Ok(Inventory);
    }

    /// <summary>
    /// Obtiene todos los inventario.
    /// </summary>
    [HttpGet("/")]
    public async Task<ActionResult<IEnumerable<InventoryWithProductDto>>> GetAll()
    {
        var inventories = await _repository.GetAllAsync();
        var result = new List<InventoryWithProductDto>();

        foreach (var inv in inventories)
        {
            var response = await _productRequestClient.GetResponse<IProductResponse>(new
            {
                ProductId = inv.ProductId
            });

            var product = response.Message;

            result.Add(new InventoryWithProductDto
            {
                ProductId = inv.ProductId,
                Quantity = inv.Quantity,
                Product = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                }
            });
        }

        return Ok(result);
    }

    /// <summary>
    /// Obtiene un inventario por su id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryWithProductDto>> GetById(int id)
    {
        var inventory = await _repository.GetByIdAsync(id);
        if (inventory == null) return NotFound();

        var response = await _productRequestClient.GetResponse<IProductResponse>(new
        {
            ProductId = inventory.ProductId
        });

        var product = response.Message;

        var result = new InventoryWithProductDto
        {
            ProductId = inventory.ProductId,
            Quantity = inventory.Quantity,
            Product = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            }
        };

        return Ok(result);
    }

    /// <summary>
    /// Crea un nuevo inventario.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Core.Inventory>> Create(Core.Inventory inventory)
    {
        await _repository.AddAsync(inventory);
        return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, inventory);
    }

    /// <summary>
    /// Actualiza un inventario existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Core.Inventory inventory)
    {
        if (id != inventory.Id) return BadRequest();
        await _repository.UpdateAsync(inventory);
        return NoContent();
    }

    /// <summary>
    /// Elimina un inventario por su id.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
