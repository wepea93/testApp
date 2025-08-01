using System.Threading.Tasks;
using InventoryService.Core.Contracts;
using MassTransit;

namespace InventoryService.Infrastructure;

public class ProductCreatedConsumer : IConsumer<IProductCreated>
{
    
    private readonly InventoryRepository _repository;

    public ProductCreatedConsumer(InventoryRepository repository)
    {
        _repository = repository;
    }

    async Task IConsumer<IProductCreated>.Consume(ConsumeContext<IProductCreated> context)
    {
        await _repository.AddAsync(new InventoryService.Core.Inventory
        {
            ProductId = context.Message.ProductId,
            Quantity = 0
        });
    }
}