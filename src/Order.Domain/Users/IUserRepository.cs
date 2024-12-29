using Order.Domain.Common.Interfaces;

namespace Order.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    public Task<User> AddAsync(User user);
}