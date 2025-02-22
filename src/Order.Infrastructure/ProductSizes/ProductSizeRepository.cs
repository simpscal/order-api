using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Common.Enums;
using Order.Domain.ProductSizes;
using Order.Infrastructure.Common;
using Order.Shared.Constants;
using Order.Shared.Extensions;

namespace Order.Infrastructure.ProductSizes;

public class ProductSizeRepository(IMemoryCache memoryCache, AppDbContext appDbContext) :
    CachedRepository<ProductSize>(memoryCache, appDbContext),
    IProductSizeRepository
{
    public async Task<ProductSize> GetAsync(SizeType sizeType)
    {
        var productSizes = await GetCachedList(CacheKeys.ProductSizes);

        return productSizes
            .First(s => s.Name == sizeType.GetStringValue());
    }

    public async Task<IEnumerable<ProductSize>> GetListAsync(IEnumerable<SizeType> sizeTypes)
    {
        var productSizes = await GetCachedList(CacheKeys.ProductSizes);

        return productSizes
            .Where(productSize => sizeTypes.Any(sizeType => sizeType.GetStringValue() == productSize.Name));
    }
}