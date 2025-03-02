using Microsoft.EntityFrameworkCore;

using Order.Domain.Common;
using Order.Domain.Common.Interfaces;
using Order.Shared.Constants;
using Order.Shared.Models;

namespace Order.Infrastructure.Common;

public class Repository<T>(AppDbContext appDbContext) : IRepository<T>
    where T : Entity
{
    protected readonly DbSet<T> _dbSet = appDbContext.Set<T>();

    public Task<T?> FindByExpressionAsync(ISpecification<T> specification)
    {
        var queryable = _dbSet.AsQueryable();

        foreach (var includeExpression in specification.Includes)
        {
            queryable = queryable.Include(includeExpression);
        }

        return queryable.Where(specification.ToExpression()).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> FilterByExpressionAsync(ISpecification<T> specification)
    {
        var queryable = _dbSet.AsQueryable().Where(specification.ToExpression());

        foreach (var includeExpression in specification.Includes)
        {
            queryable = queryable.Include(includeExpression);
        }

        return await queryable.ToListAsync();
    }

    public async Task<IEnumerable<T>> FilterByRawAsync(ISpecification<T> specification)
    {
        var queryable = _dbSet.FromSqlRaw(specification.ToRawSql());

        foreach (var includeExpression in specification.Includes)
        {
            queryable = queryable.Include(includeExpression);
        }

        return await queryable.ToListAsync();
    }

    public async Task<PagedResult<T>> FilterPagedByExpressionAsync(
        ISpecification<T> specification,
        Pagination pagination)
    {
        var queryable = _dbSet.AsQueryable().Where(specification.ToExpression());
        var totalCount = await queryable.CountAsync();

        queryable = queryable.Skip(pagination.Skip).Take(pagination.Take);

        foreach (var includeExpression in specification.Includes)
        {
            queryable = queryable.Include(includeExpression);
        }

        var items = await queryable.ToListAsync();

        return new PagedResult<T>() { TotalCount = totalCount, Items = items, };
    }

    public async Task<PagedResult<T>> FilterPagedByRawAsync(
        ISpecification<T> specification,
        Pagination pagination)
    {
        var queryable = _dbSet.FromSqlRaw(specification.ToRawSql());
        var totalCount = await queryable.CountAsync();

        queryable = queryable.Skip(pagination.Skip).Take(pagination.Take);

        foreach (var includeExpression in specification.Includes)
        {
            queryable = queryable.Include(includeExpression);
        }

        var items = await queryable.ToListAsync();

        return new PagedResult<T>() { TotalCount = totalCount, Items = items, };
    }

    public async Task DeleteAsync(Guid id, bool softDelete = false)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
        {
            throw new Exception(AppErrors.NotFound);
        }

        if (softDelete)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
        }
        else
        {
            _dbSet.Remove(entity);
        }
    }

    public void DeleteAllSoftDeleted(TimeSpan olderThan)
    {
        var deletedRecords =
            _dbSet.Where(entity => entity.IsDeleted && DateTime.UtcNow - entity.DeletedAt >= olderThan);

        _dbSet.RemoveRange(deletedRecords);
    }

    public async Task<int> SaveChangesAsync()
    {
        var result = await appDbContext.SaveChangesAsync();

        if (result < 1)
        {
            throw new Exception(AppErrors.DumbError);
        }

        return result;
    }
}