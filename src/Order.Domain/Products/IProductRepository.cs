using Order.Domain.Interfaces;

namespace Order.Domain.Products;

public interface IProductRepository : IBaseRepository
{
    public Task<string> CreateProductAsync(Product product);
    public Task<IEnumerable<Product>> GetAllProductsAsync();
}