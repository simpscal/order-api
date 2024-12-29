using MediatR;

using Microsoft.AspNetCore.Mvc;

using Order.Application.Products.Commands.CreateProduct;
using Order.Application.Products.Queries.ListProducts;
using Order.Shared.Models;

namespace Order.Api.Controllers;

[Route("api/products")]
public class ProductsController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<string> AddProduct(CreateProductCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("filter")]
    public async Task<PagedResult<ListProductsDto>> GetProducts([FromBody] ListProductsQuery request)
    {
        var products = await mediator.Send(request);

        return products;
    }
}