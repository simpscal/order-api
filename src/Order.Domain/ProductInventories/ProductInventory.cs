using Order.Domain.Common;
using Order.Domain.ProductColors;
using Order.Domain.Products;
using Order.Domain.ProductSizes;

namespace Order.Domain.ProductInventories;

public class ProductInventory : Entity
{
    public int AvailableStock { get; set; }

    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public required Guid ProductColorId { get; set; }
    public ProductColor? ProductColor { get; set; }

    public required Guid ProductSizeId { get; set; }
    public ProductSize? ProductSize { get; set; }
}