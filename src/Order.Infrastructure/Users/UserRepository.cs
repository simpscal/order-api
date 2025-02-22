using Microsoft.AspNetCore.Identity;

using Order.Domain.Users;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Users;

public class UserRepository(AppDbContext appDbContext) :
    Repository<User>(appDbContext),
    IUserRepository
{
    public async Task<User> AddAsync(User user)
    {
        var passwordHasher = new PasswordHasher<object>();
        var hashPassword = passwordHasher.HashPassword(new object(), user.Password);

        var newUser = new User
        {
            Email = user.Email,
            Password = hashPassword,
            RoleId = user.RoleId,
        };

        await _dbSet.AddAsync(newUser);

        return user;
    }
}