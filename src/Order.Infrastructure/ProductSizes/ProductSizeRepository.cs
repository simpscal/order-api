using Microsoft.Extensions.Caching.Memory;

using Order.Domain.ProductSizes;
using Order.Infrastructure.Common;
using Order.Shared.Constants;

namespace Order.Infrastructure.ProductSizes;

public class ProductSizeRepository(IMemoryCache memoryCache, AppDbContext appDbContext) :
    CachedRepository<ProductSize>(memoryCache, appDbContext),
    IProductSizeRepository
{
    public async Task<ProductSize> GetAsync(string name)
    {
        var productSizes = await GetCachedList(CacheKeys.ProductSizes);

        return productSizes.First(s => s.Name == name);
    }

    public async Task<IEnumerable<ProductSize>> GetListAsync(IEnumerable<string> names)
    {
        var productSizes = await GetCachedList(CacheKeys.ProductSizes);

        return productSizes.Where(productSize => names.Any(sizeType => sizeType == productSize.Name));
    }
}