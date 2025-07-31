using InventoryService.Core;

namespace InventoryService.Infrastructure;

public class InventoryRepository : Repository<InventoryService.Core.Inventory>
{
    public InventoryRepository(InventoryDbContext context) : base(context)
    {

    }
}
