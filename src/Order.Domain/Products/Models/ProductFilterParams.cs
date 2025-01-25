using Order.Shared.Models;

namespace Order.Domain.Products.Models;

public record ProductFilterParams : FilterParams
{
    public decimal? Price { get; set; }
}