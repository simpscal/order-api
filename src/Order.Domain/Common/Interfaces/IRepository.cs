using Order.Shared.Models;

namespace Order.Domain.Common.Interfaces;

public interface IRepository<T>
    where T : Entity
{
    Task<T?> FindByExpressionAsync(ISpecification<T> specification);
    Task<IEnumerable<T>> FilterByExpressionAsync(ISpecification<T> specification);
    Task<IEnumerable<T>> FilterByRawAsync(ISpecification<T> specification);
    Task<PagedResult<T>> FilterPagedByExpressionAsync(ISpecification<T> specification, Pagination pagination);
    Task<PagedResult<T>> FilterPagedByRawAsync(ISpecification<T> specification, Pagination pagination);
    Task DeleteAsync(Guid id, bool softDelete = false);
    void DeleteAllSoftDeleted(TimeSpan olderThan);
    Task<int> SaveChangesAsync();
}