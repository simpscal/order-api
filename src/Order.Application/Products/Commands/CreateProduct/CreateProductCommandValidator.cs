using FluentValidation;

using Order.Domain.Common.Constants;
using Order.Shared.Extensions;

namespace Order.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero");

        RuleFor(product => product.Category)
            .Must(category => category.ExistsInConstant<Categories>())
            .WithMessage("Category does not exist");

        RuleFor(product => product.SubCategory)
            .Must(subCategory => subCategory.ExistsInConstant<SubCategories>())
            .WithMessage("Sub Category does not exist");

        RuleFor(product => product.Colors).Must(colors => colors.Length > 0)
            .WithMessage("Colors are empty")
            .Must(colors => colors.All(color => color.ExistsInConstant<Colors>()))
            .WithMessage("Colors do not exist");

        RuleFor(product => product.Sizes).Must(sizes => sizes.Length > 0)
            .WithMessage("Sizes are empty")
            .Must(sizes => sizes.All(size => size.ExistsInConstant<Sizes>()))
            .WithMessage("Sizes do not exist");

        RuleFor(product => product)
            .Must(product => product.Colors.All(color => product.Inventories.ContainsKey(color)))
            .WithMessage("Color does not exist in the inventories");

        RuleFor(product => product)
            .Must(product => product.Sizes.All(size =>
                product.Inventories.Keys
                    .All(inventoryColor => product.Inventories[inventoryColor].ContainsKey(size))))
            .WithMessage("Size does not exist in the inventories");

        RuleFor(product => product.ImagesByColor)
            .NotNull()
            .WithMessage("Images are empty");

        RuleFor(product => product)
            .Must(product => product.Colors.All(color => product.ImagesByColor.ContainsKey(color)))
            .WithMessage("Color does not exist in the images");
    }
}