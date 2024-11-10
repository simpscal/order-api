using MediatR;

using Order.Application.Common.Repositories;
using Order.Domain.Products;

namespace Order.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, string>
{
    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = await productRepository.CreateProductAsync(
            new Product { Name = request.Name, Price = request.Price });

        await productRepository.SaveChangesAsync();

        return productId;
    }
}

public record CreateProductCommand(string Name, decimal Price) : IRequest<string>;