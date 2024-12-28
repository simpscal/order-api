using Order.Domain.Interfaces;

namespace Order.Domain.Users;

public interface IUserRepository : IBaseRepository
{
    public Task<User> GetUserAsync(string email, string password);
    public Task<User> CreateUserAsync(string email, string password);
}