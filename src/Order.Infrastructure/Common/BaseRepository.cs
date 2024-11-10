using Order.Application.Common.Constants;
using Order.Application.Common.Repositories;

namespace Order.Infrastructure.Common;

public class BaseRepository(AppDbContext appDbContext) : IBaseRepository
{
    public async Task<int> SaveChangesAsync()
    {
        var result = await appDbContext.SaveChangesAsync();

        if (result < 1)
        {
            throw new Exception(AppErrorConstants.DumbError);
        }

        return result;
    }
}