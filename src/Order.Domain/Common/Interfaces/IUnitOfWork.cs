using Order.Domain.Categories;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;
using Order.Domain.Users;

namespace Order.Domain.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; protected set; }
    IProductColorRepository ProductColorRepository { get; protected set; }
    IProductSizeRepository ProductSizeRepository { get; protected set; }
    ICategoryRepository CategoryRepository { get; protected set; }
    ISubCategoryRepository SubCategoryRepository { get; protected set; }
    IProductInventoryRepository ProductInventoryRepository { get; protected set; }

    IUserRepository UserRepository { get; protected set; }

    public Task<int> CommitAsync();
}