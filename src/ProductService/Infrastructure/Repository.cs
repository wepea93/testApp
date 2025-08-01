using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Core;
using ProductService.Core.Contracts;

namespace ProductService.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async virtual Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async virtual Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async virtual Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async virtual Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async virtual Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}