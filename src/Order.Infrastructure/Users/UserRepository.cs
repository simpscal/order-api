using Microsoft.EntityFrameworkCore;

using Order.Application.Common.Repositories;
using Order.Domain.Users;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Users;

public class UserRepository(AppDbContext appDbContext) : BaseRepository(appDbContext), IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<User> GetUser(string email, string password)
    {
        var user = await _appDbContext.Users.Where(user => user.Email == email).FirstAsync();

        if (user == null || user.Password != password)
        {
            throw new Exception("Invalid email or password");
        }

        return user;
    }
}