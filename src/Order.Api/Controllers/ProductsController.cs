using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Order.Application.Common.Interfaces;
using Order.Application.Products.Commands.CreateProduct;
using Order.Application.Products.Commands.DeleteProduct;
using Order.Application.Products.Queries.Products;
using Order.Domain.Common.Constants;
using Order.Shared.Models;

namespace Order.Api.Controllers;

[Route("api/products")]
public class ProductsController(IMediator mediator, IFileStorageService fileStorageService)
    : ApiController
{
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public Task<string> AddProduct(CreateProductCommand request)
    {
        return mediator.Send(request);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public Task DeleteProduct(string id)
    {
        return mediator.Send(new DeleteProductCommand(id));
    }

    [HttpPost("filter")]
    public Task<PagedResult<ProductDto>> GetProducts([FromBody] ProductsQuery request)
    {
        return mediator.Send(request);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("presigned-url")]
    public Task<string> GetPresignedUrl([FromBody] PresignedUrl request)
    {
        return fileStorageService.GetPresignedUrlAsync(
            request.FileName,
            "products",
            request.ContentType,
            TimeSpan.FromMinutes(15));
    }
}