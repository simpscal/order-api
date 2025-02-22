using System.Linq.Expressions;

using Order.Domain.Common.Specifications;

namespace Order.Domain.Users.Specifications;

public class ExistingUserSpecification : Specification<User>
{
    private readonly string _email;

    public ExistingUserSpecification(string email)
    {
        this._email = email;

        AddInclude(user => user.Role!);
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email.ToLower() == _email.ToLower();
    }
}