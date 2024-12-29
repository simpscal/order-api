using Microsoft.EntityFrameworkCore;

using Order.Application.Common.Constants;
using Order.Domain.Common.Interfaces;

namespace Order.Infrastructure.Common;

public class Repository<T>(AppDbContext appDbContext) : IRepository<T>
    where T : class
{
    protected readonly DbSet<T> _dbSet = appDbContext.Set<T>();

    public Task<T> FindByExpressionAsync(ISpecification<T> specification)
    {
        var queryable = _dbSet.AsQueryable();

        return queryable.Where(specification.ToExpression()).FirstAsync();
    }

    public async Task<IEnumerable<T>> FilterByExpressionAsync(ISpecification<T> specification)
    {
        var queryable = _dbSet.AsQueryable();

        return await queryable.Where(specification.ToExpression()).ToListAsync();
    }

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