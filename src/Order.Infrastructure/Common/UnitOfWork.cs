using Microsoft.Extensions.Caching.Memory;

using Order.Domain.Categories;
using Order.Domain.Common.Interfaces;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;
using Order.Domain.Users;
using Order.Infrastructure.Categories;
using Order.Infrastructure.ProductColors;
using Order.Infrastructure.ProductInventories;
using Order.Infrastructure.Products;
using Order.Infrastructure.ProductSizes;
using Order.Infrastructure.SubCategories;
using Order.Infrastructure.Users;

namespace Order.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    public IProductRepository ProductRepository { get; set; }
    public IProductColorRepository ProductColorRepository { get; set; }
    public IProductSizeRepository ProductSizeRepository { get; set; }
    public ICategoryRepository CategoryRepository { get; set; }
    public ISubCategoryRepository SubCategoryRepository { get; set; }
    public IProductInventoryRepository ProductInventoryRepository { get; set; }
    public IUserRepository UserRepository { get; set; }

    private readonly AppDbContext _appDbContext;

    public UnitOfWork(IMemoryCache memoryCache, AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        ProductRepository = new ProductRepository(_appDbContext);
        ProductColorRepository = new ProductColorRepository(memoryCache, _appDbContext);
        ProductSizeRepository = new ProductSizeRepository(memoryCache, _appDbContext);
        CategoryRepository = new CategoryRepository(_appDbContext);
        SubCategoryRepository = new SubCategoryRepository(_appDbContext);
        ProductInventoryRepository = new ProductInventoryRepository(_appDbContext);
        UserRepository = new UserRepository(_appDbContext);
    }

    public async Task<int> CommitAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}