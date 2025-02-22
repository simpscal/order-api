using Order.Domain.Common;

namespace Order.Domain.SubCategories;

public class SubCategory : Entity
{
    public required string Name { get; set; }
}