using Order.Domain.Common.Enums;
using Order.Domain.Common.Interfaces;

namespace Order.Domain.ProductColors;

public interface IProductColorRepository : IRepository<ProductColor>
{
    public Task<ProductColor> GetAsync(ColorType colorType);
    public Task<IEnumerable<ProductColor>> GetListAsync(IEnumerable<ColorType> colorTypes);
}