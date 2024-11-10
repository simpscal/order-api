using Order.Domain.Common;

namespace Order.Domain.Products;

public class Product : Entity
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}