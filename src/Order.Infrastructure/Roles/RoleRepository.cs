using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Common.Enums;
using Order.Domain.Roles;
using Order.Infrastructure.Common;
using Order.Shared.Constants;
using Order.Shared.Extensions;

namespace Order.Infrastructure.Roles;

public class RoleRepository(IMemoryCache memoryCache, AppDbContext appDbContext) :
    CachedRepository<Role>(memoryCache, appDbContext),
    IRoleRepository
{
    public async Task<Role> GetAsync(RoleType roleType)
    {
        var roles = await GetCachedList(CacheKeys.Roles);

        return roles.First(role => role.Name == roleType.GetStringValue());
    }

    public async Task<IEnumerable<Role>> GetListAsync(IEnumerable<RoleType> roleTypes)
    {
        var roles = await GetCachedList(CacheKeys.Roles);

        return roles.Where(role => roleTypes.Any(roleType => roleType.GetStringValue() == role.Name));
    }
}