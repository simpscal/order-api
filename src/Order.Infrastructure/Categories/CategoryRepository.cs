using Microsoft.EntityFrameworkCore;

using Order.Domain.Categories;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.Categories;

public class CategoryRepository(AppDbContext appDbContext) :
    Repository<Category>(appDbContext),
    ICategoryRepository
{
    public async Task<Guid> GetIdAsync(string name)
    {
        var category = await _dbSet
            .Where(category => category.Name == name)
            .FirstAsync();

        return category.Id;
    }
}