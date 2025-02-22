using Microsoft.EntityFrameworkCore;

using Order.Domain.SubCategories;
using Order.Infrastructure.Common;

namespace Order.Infrastructure.SubCategories;

public class SubCategoryRepository(AppDbContext appDbContext) :
    Repository<SubCategory>(appDbContext),
    ISubCategoryRepository
{
    public async Task<Guid> GetIdAsync(string name)
    {
        var subCategory = await _dbSet
            .Where(subCategory => subCategory.Name == name)
            .FirstAsync();

        return subCategory.Id;
    }
}