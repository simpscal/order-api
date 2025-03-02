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
        AddInclude(product => product.SubCategory!);
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        return product =>
            product.IsDeleted == false &&
            (_filterParams.Price == null ||
             product.Price == _filterParams.Price) &&
            (_filterParams.Colors == null ||
             product.ProductColors.Any(productColor =>
                 _filterParams.Colors.Any(color => color == productColor.Name))) &&
            (_filterParams.Sizes == null ||
             product.ProductSizes.Any(productSize => _filterParams.Sizes.Any(size => size == productSize.Name))) &&
            (_filterParams.SubCategories == null ||
             _filterParams.SubCategories.Any(subCategory => subCategory == product.SubCategory!.Name)) &&
            product.Name.Contains(_filterParams.Keyword ?? string.Empty);
    }
}