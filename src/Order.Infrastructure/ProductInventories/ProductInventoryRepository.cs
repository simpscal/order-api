using Order.Domain.ProductInventories;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.ProductInventories;

public class ProductInventoryRepository(AppDbContext appDbContext) :
    Repository<ProductInventory>(appDbContext),
    IProductInventoryRepository
{
    public async Task<string> AddAsync(ProductInventory productInventory)
    {
        var result = await _dbSet.AddAsync(productInventory);

        return result.Entity.Id.ToString();
    }
}