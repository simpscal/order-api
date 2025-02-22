using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Roles;
using Order.Infrastructure.Common;
using Order.Shared.Constants;

namespace Order.Infrastructure.Roles;

public class RoleRepository(IMemoryCache memoryCache, AppDbContext appDbContext) :
    CachedRepository<Role>(memoryCache, appDbContext),
    IRoleRepository
{
    public async Task<Role> GetAsync(string name)
    {
        var roles = await GetCachedList(CacheKeys.Roles);

        return roles.First(role => role.Name == name);
    }

    public async Task<IEnumerable<Role>> GetListAsync(IEnumerable<string> names)
    {
        var roles = await GetCachedList(CacheKeys.Roles);

        return roles.Where(role => names.Any(name => name == role.Name));
    }
}