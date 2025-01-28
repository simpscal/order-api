using MediatR;

using Order.Domain.Categories;
using Order.Domain.Common.Enums;
using Order.Domain.Common.Interfaces;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;
using Order.Shared.Extensions;

namespace Order.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    ISubCategoryRepository subCategoryRepository,
    IProductColorRepository productColorRepository,
    IProductSizeRepository productSizeRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, string>
{
    public async Task<string> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var (productColors, productSizes, categoryId, subCategoryId) = await GetDataSource(request);

        var productId = await unitOfWork.ProductRepository.AddAsync(
            new Product
            {
                Name = request.Name,
                Price = request.Price,
                ProductColors = productColors,
                ProductSizes = productSizes,
                CategoryId = categoryId,
                SubCategoryId = subCategoryId,
                ImagesByColor = request.ImagesByColor,
            });

        var productInventories = GenerateProductInventories(
            productColors,
            productSizes,
            request,
            productId);

        await unitOfWork.ProductInventoryRepository.AddRangeAsync(productInventories);

        await unitOfWork.CommitAsync();

        return productId.ToString();
    }

    private async
        Task<(
            List<ProductColor> ProductColors,
            List<ProductSize> ProductSizes,
            Guid CategoryId,
            Guid SubCategoryId)> GetDataSource(
            CreateProductCommand request)
    {
        var productColorsTask = productColorRepository.GetListAsync(
            request.Colors.Select(color => color.ToEnum<ColorType>()));
        var productSizesTask = productSizeRepository.GetListAsync(
            request.Sizes.Select(size => size.ToEnum<SizeType>()));

        var categoryIdTask = categoryRepository.GetIdAsync(
            request.Category.ToEnum<CategoryType>());
        var subCategoryIdTask = subCategoryRepository.GetIdAsync(
            request.SubCategory.ToEnum<SubCategoryType>());

        await Task.WhenAll(productColorsTask, productSizesTask, categoryIdTask, subCategoryIdTask);

        return
        (
            productColorsTask.Result.ToList(),
            productSizesTask.Result.ToList(),
            categoryIdTask.Result,
            subCategoryIdTask.Result);
    }

    private IEnumerable<ProductInventory> GenerateProductInventories(
        IEnumerable<ProductColor> productColors,
        IEnumerable<ProductSize> productSizes,
        CreateProductCommand request,
        Guid productId)
    {
        return productColors.SelectMany(productColor => productSizes.Select(
            productSize =>
            {
                var stock = request.Inventories[productColor.Name][productSize.Name];

                return new ProductInventory
                {
                    ProductId = productId,
                    ProductColorId = productColor.Id,
                    ProductSizeId = productSize.Id,
                    AvailableStock = stock,
                };
            }));
    }
}

public record CreateProductCommand(
    string Name,
    decimal Price,
    string[] Colors,
    string[] Sizes,
    string SubCategory,
    string Category,
    Dictionary<string, string[]> ImagesByColor,
    Dictionary<string, Dictionary<string, int>> Inventories)
    : IRequest<string>;