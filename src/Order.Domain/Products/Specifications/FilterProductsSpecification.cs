using System.Linq.Expressions;

using Order.Domain.Common.Specifications;
using Order.Domain.Products.Models;

namespace Order.Domain.Products.Specifications;

public class FilterProductsSpecification(ProductFilterParams filterParams) : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return product =>
            (filterParams.Price == null || product.Price == filterParams.Price) &&
            product.Name.Contains(filterParams.Keyword ?? string.Empty);
    }
}