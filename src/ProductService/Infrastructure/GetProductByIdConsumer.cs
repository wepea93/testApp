using System.Threading.Tasks;
using ProductService.Core.Contracts;
using MassTransit;
using ProductService.Infrastructure;
public class GetProductByIdConsumer : IConsumer<IGetProductById>
{
    private readonly ProductRepository _repository;

    public GetProductByIdConsumer(ProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<IGetProductById> context)
    {
        var product = await _repository.GetByIdAsync(context.Message.ProductId);

        if (product == null)
        {
            // Puedes responder null o lanzar excepción
            await context.RespondAsync<IProductResponse>(null);
            return;
        }

        await context.RespondAsync<IProductResponse>(new
        {
            Id = product.Id,
            product.Name,
            product.Description,
            product.Price
        });
    }
}
