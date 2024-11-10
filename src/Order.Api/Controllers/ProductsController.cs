using MediatR;

using Microsoft.AspNetCore.Mvc;

using Order.Application.Products.Commands.CreateProduct;
using Order.Application.Products.Queries.ListProducts;

namespace Order.Api.Controllers;

public class ProductsController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<string> CreateProduct(CreateProductCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpGet]
    public async Task<IEnumerable<ListProductsDto>> GetProducts()
    {
        var products = await mediator.Send(new ListProductsQuery());

        return products;
    }
}