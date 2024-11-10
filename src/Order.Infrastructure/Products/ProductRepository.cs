using Microsoft.EntityFrameworkCore;

using Order.Application.Common.Repositories;
using Order.Domain.Products;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Products;

public class ProductRepository(AppDbContext appDbContext) : BaseRepository(appDbContext), IProductRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<string> CreateProductAsync(Product product)
    {
        var result = await _appDbContext.Products.AddAsync(product);

        return result.Entity.Id.ToString();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _appDbContext.Products.ToListAsync();
    }
}