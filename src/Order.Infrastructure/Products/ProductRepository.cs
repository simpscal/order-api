using Microsoft.EntityFrameworkCore;

using Order.Domain.Products;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Products;

public class ProductRepository(AppDbContext appDbContext) : Repository<Product>(appDbContext), IProductRepository
{
    public async Task<string> AddAsync(Product product)
    {
        var result = await _dbSet.AddAsync(product);

        return result.Entity.Id.ToString();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}