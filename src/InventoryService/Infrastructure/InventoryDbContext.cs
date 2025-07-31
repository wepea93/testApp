using Microsoft.EntityFrameworkCore;
using InventoryService.Core;

namespace InventoryService.Infrastructure;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryService.Core.Inventory>().HasData(
            new InventoryService.Core.Inventory { Id = 1, ProductId = 1, Quantity = 5 },
            new InventoryService.Core.Inventory { Id = 2, ProductId = 2, Quantity = 10 },
            new InventoryService.Core.Inventory { Id = 3, ProductId = 3, Quantity = 0 }
        );
    }
}
