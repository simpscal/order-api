using AutoMapper;

using MediatR;

using Order.Domain.Products;
using Order.Domain.Products.Models;
using Order.Domain.Products.Specifications;
using Order.Shared.Models;

namespace Order.Application.Products.Queries.Products;

public class ProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
    : IRequestHandler<ProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(
        ProductsQuery request,
        CancellationToken cancellationToken)
    {
        var filterProductsSpecification = new FilterProductsSpecification(request);
        var pagedResult = await productRepository.FilterPagedByExpressionAsync(filterProductsSpecification, request);

        return new PagedResult<ProductDto>()
        {
            TotalCount = pagedResult.TotalCount,
            Items = pagedResult.Items.Select(mapper.Map<ProductDto>),
        };
    }
}

public record ProductsQuery : ProductFilterParams, IRequest<PagedResult<ProductDto>>;