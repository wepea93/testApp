using Microsoft.AspNetCore.Mvc;
using ProductService.Core;
using ProductService.Infrastructure;

namespace ProductService.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene todos los productos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductService.Core.Product>>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Obtiene un producto por su id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductService.Core.Product>> GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Crea un nuevo producto.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProductService.Core.Product>> Create(ProductService.Core.Product product)
    {
        await _repository.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Actualiza un producto existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductService.Core.Product product)
    {
        if (id != product.Id) return BadRequest();
        await _repository.UpdateAsync(product);
        return NoContent();
    }

    /// <summary>
    /// Elimina un producto por su id.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
