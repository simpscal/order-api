using Order.Domain.Common.Interfaces;

namespace Order.Domain.Roles;

public interface IRoleRepository : IRepository<Role>
{
    public Task<Role> GetAsync(string name);
    public Task<IEnumerable<Role>> GetListAsync(IEnumerable<string> names);
}