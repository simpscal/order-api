using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Order.Domain.Categories;
using Order.Domain.Common.Interfaces;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;
using Order.Domain.Users;
using Order.Infrastructure.Categories;
using Order.Infrastructure.Common;
using Order.Infrastructure.ProductColors;
using Order.Infrastructure.ProductInventories;
using Order.Infrastructure.Products;
using Order.Infrastructure.ProductSizes;
using Order.Infrastructure.SubCategories;
using Order.Infrastructure.Users;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Transient);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
        services.AddScoped<IProductColorRepository, ProductColorRepository>();
        services.AddScoped<IProductInventoryRepository, ProductInventoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}