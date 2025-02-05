using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Common.Enums;
using Order.Domain.ProductColors;
using Order.Infrastructure.Common;
using Order.Shared.Constants;
using Order.Shared.Extensions;

namespace Order.Infrastructure.ProductColors;

public class ProductColorRepository(IMemoryCache memoryCache, AppDbContext appDbContext)
    : Repository<ProductColor>(appDbContext), IProductColorRepository
{
    public async Task<ProductColor> GetAsync(ColorType colorType)
    {
        var productColors = await GetCachedProductColors();

        var result = productColors
            .First(productColor => productColor.Name == colorType.GetStringValue());

        return result;
    }

    public async Task<IEnumerable<ProductColor>> GetListAsync(IEnumerable<ColorType> colorTypes)
    {
        var productColors = await GetCachedProductColors();

        var colors = colorTypes.Select(color => color.GetStringValue());

        var result = productColors
            .Where(productColor => colors.Contains(productColor.Name));

        return result;
    }

    private async Task<IEnumerable<ProductColor>> GetCachedProductColors()
    {
        var productColors = memoryCache.Get<IEnumerable<ProductColor>>(CacheKeys.ProductColors);

        if (productColors != null)
        {
            return productColors;
        }

        productColors = await _dbSet.ToListAsync();
        memoryCache.Set(CacheKeys.ProductColors, productColors, TimeSpan.FromHours(1));

        return productColors;
    }
}