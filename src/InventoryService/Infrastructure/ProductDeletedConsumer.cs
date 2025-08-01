using System.Threading.Tasks;
using InventoryService.Core.Contracts;
using MassTransit;

namespace InventoryService.Infrastructure;

public class ProductDeletedConsumer : IConsumer<IProductDeleted>
{    
    private readonly InventoryRepository _repository;

    public ProductDeletedConsumer(InventoryRepository repository)
    {
        _repository = repository;
    }

    async Task IConsumer<IProductDeleted>.Consume(ConsumeContext<IProductDeleted> context)
    {      
        await _repository.DeleteAsync(context.Message.ProductId);        
    }
}