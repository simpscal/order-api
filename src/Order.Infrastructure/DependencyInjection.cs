using Amazon.S3;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Order.Domain.Categories;
using Order.Domain.Common.Interfaces;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.Roles;
using Order.Domain.SubCategories;
using Order.Domain.Users;
using Order.Infrastructure.Categories;
using Order.Infrastructure.Common;
using Order.Infrastructure.Common.Services;
using Order.Infrastructure.ProductColors;
using Order.Infrastructure.ProductInventories;
using Order.Infrastructure.Products;
using Order.Infrastructure.ProductSizes;
using Order.Infrastructure.Roles;
using Order.Infrastructure.SubCategories;
using Order.Infrastructure.Users;
using Order.Shared.Interfaces;

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
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddAWSS3();

        return services;
    }

    private static IServiceCollection AddAWSS3(this IServiceCollection services)
    {
        services.AddSingleton<IFileStorageService, S3Service>();

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            return new AmazonS3Client(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"],
                Amazon.RegionEndpoint.GetBySystemName(configuration["AWS:Region"]));
        });

        return services;
    }
}