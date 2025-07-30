using ProductService.Core;

namespace ProductService.Infrastructure;

public class ProductRepository : Repository<Product>
{
    public ProductRepository(ProductsDbContext context) : base(context)
    {

    }
}
