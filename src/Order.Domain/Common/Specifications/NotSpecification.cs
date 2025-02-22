using System.Linq.Expressions;

namespace Order.Domain.Common.Specifications;

public class NotSpecification<T>(Specification<T> specification)
    : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = specification.ToExpression();
        var parameter = expression.Parameters[0];
        var body = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}