using System.ComponentModel;

using Order.Domain.Common;
using Order.Domain.Products;

namespace Order.Domain.ProductColors;

public class ProductColor : Entity
{
    [Description("ColorType")]
    public required string Name { get; set; }

    public required string Code { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}