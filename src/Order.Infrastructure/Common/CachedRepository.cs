using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using Order.Shared.Constants;

namespace Order.Infrastructure.Common;

public class CachedRepository<T>(IMemoryCache memoryCache, AppDbContext appDbContext)
    : Repository<T>(appDbContext)
    where T : class
{
    protected async Task<IEnumerable<T>> GetCachedList(string cacheKey)
    {
        var cachedList = memoryCache.Get<IEnumerable<T>>(cacheKey);

        if (cachedList != null)
        {
            return cachedList;
        }

        cachedList = await _dbSet.ToListAsync();
        memoryCache.Set(CacheKeys.Roles, cachedList, TimeSpan.FromHours(1));

        return cachedList;
    }
}