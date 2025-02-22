using Order.Domain.Common.Interfaces;

namespace Order.Domain.ProductSizes;

public interface IProductSizeRepository : IRepository<ProductSize>
{
    public Task<ProductSize> GetAsync(string name);
    public Task<IEnumerable<ProductSize>> GetListAsync(IEnumerable<string> names);
}