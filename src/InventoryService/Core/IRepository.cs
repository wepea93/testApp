namespace InventoryService.Core;

public interface IRepository<T> where T : class
{
    Task<T?> GetByProductIdAsync(int productId);
    Task UpdateAsync(T entity);

}