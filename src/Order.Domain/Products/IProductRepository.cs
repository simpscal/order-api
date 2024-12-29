using Order.Domain.Common.Interfaces;

namespace Order.Domain.Products;

public interface IProductRepository : IRepository<Product>
{
    public Task<string> AddAsync(Product product);
    public Task<IEnumerable<Product>> GetAllAsync();
}