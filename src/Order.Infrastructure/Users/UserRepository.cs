using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Order.Application.Common.Repositories;
using Order.Domain.Users;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Users;

public class UserRepository(AppDbContext appDbContext) : BaseRepository(appDbContext), IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<User> GetUserAsync(string email, string password)
    {
        var user = await _appDbContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("Invalid email or password");
        }

        var passwordHasher = new PasswordHasher<object>();
        var verificationResult = passwordHasher.VerifyHashedPassword(new object(), user.Password, password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid email or password");
        }

        return user;
    }

    public async Task<User> CreateUserAsync(string email, string password)
    {
        var existingUser = await _appDbContext.Users.Where(user => user.Email.ToLower() == email)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        var passwordHasher = new PasswordHasher<object>();
        var hashPassword = passwordHasher.HashPassword(new object(), password);

        var user = new User { Email = email, Password = hashPassword };
        _appDbContext.Users.Add(user);

        if (await SaveChangesAsync() < 1)
        {
            throw new ApplicationException("There was an error creating the user");
        }

        return user;
    }
}