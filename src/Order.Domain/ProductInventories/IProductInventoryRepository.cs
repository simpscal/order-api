using Order.Domain.Common.Interfaces;

namespace Order.Domain.ProductInventories;

public interface IProductInventoryRepository : IRepository<ProductInventory>
{
    public Task<string> AddAsync(ProductInventory productInventory);
    public Task AddRangeAsync(IEnumerable<ProductInventory> productInventories);
}