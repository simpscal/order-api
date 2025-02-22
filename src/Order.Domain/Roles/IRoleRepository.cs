using Order.Domain.Common.Enums;
using Order.Domain.Common.Interfaces;

namespace Order.Domain.Roles;

public interface IRoleRepository : IRepository<Role>
{
    public Task<Role> GetAsync(RoleType roleType);
    public Task<IEnumerable<Role>> GetListAsync(IEnumerable<RoleType> roleTypes);
}