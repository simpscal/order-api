using Order.Domain.Categories;
using Order.Domain.Common;
using Order.Domain.ProductColors;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;

namespace Order.Domain.Products;

public class Product : Entity
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string[] Images { get; set; } = [];

    public required ICollection<ProductColor> ProductColors { get; set; }
    public required ICollection<ProductSize> ProductSizes { get; set; }

    public required Guid SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }

    public required Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}