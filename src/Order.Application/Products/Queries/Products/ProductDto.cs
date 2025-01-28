namespace Order.Application.Products.Queries.Products;

public class ProductDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required Dictionary<string, string[]> ImagesByColor { get; set; }
    public required IEnumerable<string> ProductColors { get; set; }
    public required IEnumerable<string> ProductSizes { get; set; }
}