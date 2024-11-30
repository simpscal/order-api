using Order.Domain.Users;

namespace Order.Application.Common.Repositories;

public interface IUserRepository : IBaseRepository
{
    public Task<User> GetUserAsync(string email, string password);
    public Task<User> CreateUserAsync(string email, string password);
}