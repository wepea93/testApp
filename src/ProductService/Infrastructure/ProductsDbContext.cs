using Microsoft.EntityFrameworkCore;
using ProductService.Core;

namespace ProductService.Infrastructure;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "teclado", Price = 15.55m, Description = "teclado pc" },
            new Product { Id = 2, Name = "audifonos", Price = 9.79m },
            new Product { Id = 3, Name = "camara", Price = 45m, Description = "resolucionde 15MP" }
        );
    }
}
