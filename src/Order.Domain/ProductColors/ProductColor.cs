using Order.Domain.Common;
using Order.Domain.Products;

namespace Order.Domain.ProductColors;

public class ProductColor : Entity
{
    public required string Name { get; set; }

    public required string Code { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}