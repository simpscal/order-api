using Order.Domain.Products;

namespace Order.Application.Common.Repositories;

public interface IProductRepository : IBaseRepository
{
    public Task<string> CreateProductAsync(Product product);
    public Task<IEnumerable<Product>> GetAllProductsAsync();
}