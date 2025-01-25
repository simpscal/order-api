using System.ComponentModel;

using Order.Domain.Common;
using Order.Domain.Products;

namespace Order.Domain.ProductColors;

public class ProductColor : Entity
{
    [Description("ColorType")]
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = [];
}