using AutoMapper;

using MediatR;

using Order.Domain.Products;

namespace Order.Application.Products.Queries.ListProducts;

public class ListProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
    : IRequestHandler<ListProductsQuery, IEnumerable<ListProductsDto>>
{
    public async Task<IEnumerable<ListProductsDto>> Handle(
        ListProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync();

        return products.Select(product => mapper.Map<ListProductsDto>(product));
    }
}

public class ListProductsQuery : IRequest<IEnumerable<ListProductsDto>>;