using Order.Domain.Users;

namespace Order.Application.Common.Repositories;

public interface IUserRepository : IBaseRepository
{
    public Task<User> GetUser(string email, string password);
}