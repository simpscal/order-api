using System.Linq.Expressions;

using Order.Domain.Common.Interfaces;

namespace Order.Domain.Common.Specifications;

public abstract class RawSpecification<T> : ISpecification<T>
{
    public abstract string ToRawSql();
    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public Expression<Func<T, bool>> ToExpression()
    {
        return (T value) => false;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return false;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}