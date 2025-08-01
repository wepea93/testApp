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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Core.Inventory>>> GetAll()
    {
        var inventories = await _repository.    ();
        return Ok(inventories);
    }

    /// <summary>
    /// Obtiene un inventario por su id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Core.Inventory>> GetById(int id)
    {
        var inventory = await _repository.GetByIdAsync(id);
        if (inventory == null) return NotFound();
        return Ok(inventory);
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
