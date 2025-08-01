using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure;

public class InventoryRepository(InventoryDbContext context) : Repository<InventoryService.Core.Inventory>(context)
{
    public async Task<Core.Inventory> GetByProductIdAsync(int productId)
        => await _dbSet.FirstOrDefaultAsync(i => i.ProductId == productId);
    
    public async Task<bool> DeleteByProductIdAsync(int productId)
    {
        var item = await _dbSet.FirstOrDefaultAsync(i => i.ProductId == productId);

        if (item == null)
            return false;

        _dbSet.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }  
}
