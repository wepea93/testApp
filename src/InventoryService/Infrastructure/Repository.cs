using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryService.Core;
using System.Security.Cryptography.X509Certificates;

namespace InventoryService.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<T?> GetByProductIdAsync(int productId)
        => await _dbSet.FindAsync(productId);

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

}