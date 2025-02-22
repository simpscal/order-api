using Microsoft.Extensions.Caching.Memory;

using Order.Domain.ProductColors;
using Order.Infrastructure.Common;
using Order.Shared.Constants;

namespace Order.Infrastructure.ProductColors;

public class ProductColorRepository(IMemoryCache memoryCache, AppDbContext appDbContext)
    : CachedRepository<ProductColor>(memoryCache, appDbContext),
        IProductColorRepository
{
    public async Task<ProductColor> GetAsync(string name)
    {
        var productColors = await GetCachedList(CacheKeys.ProductColors);

        return productColors.First(productColor => productColor.Name == name);
    }

    public async Task<IEnumerable<ProductColor>> GetListAsync(IEnumerable<string> names)
    {
        var productColors = await GetCachedList(CacheKeys.ProductColors);

        return productColors.Where(productColor => names.Any(colorType => colorType == productColor.Name));
    }
}