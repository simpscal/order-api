using Order.Domain.Common;
using Order.Domain.ProductColors;
using Order.Domain.Products;
using Order.Domain.ProductSizes;

namespace Order.Domain.ProductInventories;

public class ProductInventory : Entity
{
    public required Guid ProductId { get; set; }
    public required string ProductColorName { get; set; }
    public required string ProductSizeName { get; set; }
    public int AvailableStock { get; set; }

    public Product? Product { get; set; }
    public ProductColor? ProductColor { get; set; }
    public ProductSize? ProductSize { get; set; }
}