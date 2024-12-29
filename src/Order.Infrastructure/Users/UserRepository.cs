using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Order.Domain.Users;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Users;

public class UserRepository(AppDbContext appDbContext) : Repository<User>(appDbContext), IUserRepository
{
    public async Task<User> AddAsync(User user)
    {
        var passwordHasher = new PasswordHasher<object>();
        var hashPassword = passwordHasher.HashPassword(new object(), user.Password);

        var newUser = new User { Email = user.Email, Password = hashPassword };
        _dbSet.Add(newUser);

        if (await SaveChangesAsync() < 1)
        {
            throw new ApplicationException("There was an error creating the user");
        }

        return user;
    }
}