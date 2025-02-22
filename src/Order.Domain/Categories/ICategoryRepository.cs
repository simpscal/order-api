using Order.Domain.Common.Interfaces;

namespace Order.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<Guid> GetIdAsync(string name);
}