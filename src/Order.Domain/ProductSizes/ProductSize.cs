using System.ComponentModel;

using Order.Domain.Common;
using Order.Domain.Products;

namespace Order.Domain.ProductSizes;

public class ProductSize : Entity
{
    [Description("SizeType")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = [];
}