using Microsoft.AspNetCore.Mvc;
using InventoryService.Core;
using InventoryService.Infrastructure;


using Microsoft.AspNetCore.Authorization;
namespace InventoryService.Api.Controllers;

[
    ApiController,
    Route("api/[controller]"),
    Produces("application/json"),
    Authorize
]
public class InventoryController : ControllerBase
{
    private readonly InventoryRepository _repository;

    public InventoryController(InventoryRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene un Inventoryo por su id.
    /// </summary>
    [HttpGet("{productId}")]
    public async Task<ActionResult<InventoryService.Core.Inventory>> GetByProductId(int productId)
    {
        var Inventory = await _repository.GetByProductIdAsync(productId);
        if (Inventory == null) return NotFound();
        return Ok(Inventory);
    }

    /// <summary>
    /// Actualiza un Inventoryo existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, InventoryService.Core.Inventory Inventory)
    {
        if (id != Inventory.Id) return BadRequest();
        await _repository.UpdateAsync(Inventory);
        return NoContent();
    }
}
