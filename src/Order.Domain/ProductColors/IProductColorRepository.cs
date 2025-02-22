using Order.Domain.Common.Interfaces;

namespace Order.Domain.ProductColors;

public interface IProductColorRepository : IRepository<ProductColor>
{
    public Task<ProductColor> GetAsync(string name);
    public Task<IEnumerable<ProductColor>> GetListAsync(IEnumerable<string> names);
}