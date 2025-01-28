using System.Linq.Expressions;

using Order.Domain.Common.Specifications;
using Order.Domain.Products.Models;

namespace Order.Domain.Products.Specifications;

public class FilterProductsSpecification : Specification<Product>
{
    private readonly ProductFilterParams _filterParams;

    public FilterProductsSpecification(ProductFilterParams filterParams)
    {
        _filterParams = filterParams;

        AddInclude(product => product.ProductColors);
        AddInclude(product => product.ProductSizes);
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        return product =>
            (_filterParams.Price == null || product.Price == _filterParams.Price) &&
            product.Name.Contains(_filterParams.Keyword ?? string.Empty);
    }
}