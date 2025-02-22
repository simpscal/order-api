using Order.Domain.Common.Interfaces;

namespace Order.Domain.SubCategories;

public interface ISubCategoryRepository : IRepository<SubCategory>
{
    public Task<Guid> GetIdAsync(string name);
}