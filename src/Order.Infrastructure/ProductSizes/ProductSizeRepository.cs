using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Common.Enums;
using Order.Domain.ProductSizes;
using Order.Infrastructure.Common;
using Order.Shared.Constants;
using Order.Shared.Extensions;

namespace Order.Infrastructure.ProductSizes;

public class ProductSizeRepository(IMemoryCache memoryCache, AppDbContext appDbContext) :
    Repository<ProductSize>(appDbContext),
    IProductSizeRepository
{
    public async Task<ProductSize> GetAsync(SizeType sizeType)
    {
        var productSizes = await GetCachedProductSizes();

        var result = productSizes
            .First(s => s.Name == sizeType.GetStringValue());

        return result;
    }

    public async Task<IEnumerable<ProductSize>> GetListAsync(IEnumerable<SizeType> sizeTypes)
    {
        var productSizes = await GetCachedProductSizes();

        var sizes = sizeTypes.Select(sizeType => sizeType.GetStringValue());

        var result = productSizes
            .Where(productSize => sizes.Contains(productSize.Name));

        return result;
    }

    private async Task<IEnumerable<ProductSize>> GetCachedProductSizes()
    {
        var productSizes = memoryCache.Get<IEnumerable<ProductSize>>(CacheKeys.ProductSizes);

        if (productSizes != null)
        {
            return productSizes;
        }

        productSizes = await _dbSet.ToListAsync();
        memoryCache.Set(CacheKeys.ProductSizes, productSizes, TimeSpan.FromHours(1));

        return productSizes;
    }
}