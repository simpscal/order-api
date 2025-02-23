using System.Linq.Expressions;

namespace Order.Domain.Common.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
    string ToRawSql();
    bool IsSatisfiedBy(T entity);

    List<Expression<Func<T, object>>> Includes { get; }
}