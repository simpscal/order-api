using System.Linq.Expressions;

namespace Order.Domain.Common.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
    List<Expression<Func<T, object>>> Includes { get; }
    bool IsSatisfiedBy(T entity);
}