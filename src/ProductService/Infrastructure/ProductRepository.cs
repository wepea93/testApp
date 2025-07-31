using ProductService.Core;

namespace ProductService.Infrastructure;

public class ProductRepository : Repository<ProductService.Core.Product>
{
    public ProductRepository(ProductsDbContext context) : base(context)
    {

    }
}
