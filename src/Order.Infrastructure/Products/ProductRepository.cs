using Microsoft.EntityFrameworkCore;

using Order.Domain.Products;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Products;

public class ProductRepository(AppDbContext appDbContext) :
    Repository<Product>(appDbContext),
    IProductRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<Guid> AddAsync(Product product)
    {
        _appDbContext.AttachRange(product.ProductColors);
        _appDbContext.AttachRange(product.ProductSizes);

        var result = await _dbSet.AddAsync(product);

        return result.Entity.Id;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}