using System.Threading.Tasks;
using MassTransit;
using ProductService.Core;
using ProductService.Core.Contracts;

namespace ProductService.Infrastructure;

public class ProductRepository : Repository<ProductService.Core.Product>
{
    private readonly IPublishEndpoint _publishEndpoint;
    public ProductRepository(ProductsDbContext context, IPublishEndpoint publishEndpoint) : base(context)
    {
        _publishEndpoint = publishEndpoint;
    }

    public override async Task AddAsync(ProductService.Core.Product entity)
    {
        await base.AddAsync(entity);
        // Additional logic specific to ProductRepository can be added here if needed
        await _publishEndpoint.Publish<IProductCreated>(new
        {
            ProductId = entity.Id
        });
    }

    public override async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(id);
        // Additional logic specific to ProductRepository can be added here if needed        
        await _publishEndpoint.Publish<IProductDeleted>(new
        {
            ProductId = id
        });
    }
}
