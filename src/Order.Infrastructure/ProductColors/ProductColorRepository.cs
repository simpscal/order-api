using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Common.Enums;
using Order.Domain.ProductColors;
using Order.Infrastructure.Common;
using Order.Shared.Constants;
using Order.Shared.Extensions;

namespace Order.Infrastructure.ProductColors;

public class ProductColorRepository(IMemoryCache memoryCache, AppDbContext appDbContext)
    : CachedRepository<ProductColor>(memoryCache, appDbContext),
        IProductColorRepository
{
    public async Task<ProductColor> GetAsync(ColorType colorType)
    {
        var productColors = await GetCachedList(CacheKeys.ProductColors);

        return productColors
            .First(productColor => productColor.Name == colorType.GetStringValue());
    }

    public async Task<IEnumerable<ProductColor>> GetListAsync(IEnumerable<ColorType> colorTypes)
    {
        var productColors = await GetCachedList(CacheKeys.ProductColors);

        return productColors
            .Where(productColor => colorTypes.Any(colorType => colorType.GetStringValue() == productColor.Name));
    }
}