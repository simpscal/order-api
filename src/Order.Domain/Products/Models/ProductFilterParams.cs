using Order.Shared.Models;

namespace Order.Domain.Products.Models;

public record ProductFilterParams : FilterParams
{
    public decimal? Price { get; set; }
    public IEnumerable<string>? Colors { get; set; }
    public IEnumerable<string>? Sizes { get; set; }
    public IEnumerable<string>? SubCategories { get; set; }
}