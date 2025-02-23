using System.ComponentModel;

using Order.Domain.Common;
using Order.Domain.Products;

namespace Order.Domain.ProductSizes;

public class ProductSize : Entity
{
    public required string Name { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}