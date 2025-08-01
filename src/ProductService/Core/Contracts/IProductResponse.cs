namespace ProductService.Core.Contracts;

public interface IProductResponse
{
    int Id { get; }
    string Name { get; }
    string Description { get; }
    decimal Price { get; }
}