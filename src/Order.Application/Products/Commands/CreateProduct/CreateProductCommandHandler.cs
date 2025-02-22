using MediatR;

using Order.Domain.Categories;
using Order.Domain.Common.Interfaces;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;

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

        var productInventories = productColors
            .SelectMany(productColor => productSizes
                .Select(productSize =>
                    new ProductInventory
                    {
                        ProductId = productId,
                        ProductColorId = productColor.Id,
                        ProductSizeId = productSize.Id,
                        AvailableStock = request.Inventories[productColor.Name][productSize.Name],
                    }));

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
        var productColorsTask = productColorRepository.GetListAsync(request.Colors);
        var productSizesTask = productSizeRepository.GetListAsync(request.Sizes);

        var categoryIdTask = categoryRepository.GetIdAsync(request.Category);
        var subCategoryIdTask = subCategoryRepository.GetIdAsync(request.SubCategory);

        await Task.WhenAll(productColorsTask, productSizesTask, categoryIdTask, subCategoryIdTask);

        return
        (
            productColorsTask.Result.ToList(),
            productSizesTask.Result.ToList(),
            categoryIdTask.Result,
            subCategoryIdTask.Result);
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