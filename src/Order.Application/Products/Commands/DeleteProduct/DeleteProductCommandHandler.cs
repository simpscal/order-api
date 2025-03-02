using MediatR;

using Order.Domain.Common.Interfaces;
using Order.Domain.Products;

namespace Order.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.ProductRepository.DeleteAsync(new Guid(request.Id), true);
        await unitOfWork.CommitAsync();
    }
}

public record DeleteProductCommand(string Id) : IRequest;