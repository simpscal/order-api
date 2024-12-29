namespace Order.Domain.Common.Interfaces;

public interface IRepository<T>
{
    Task<T> FindByExpressionAsync(ISpecification<T> specification);
    Task<IEnumerable<T>> FilterByExpressionAsync(ISpecification<T> specification);
    Task<int> SaveChangesAsync();
}