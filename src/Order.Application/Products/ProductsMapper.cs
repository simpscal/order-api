using AutoMapper;

using Order.Application.Products.Queries.Products;
using Order.Domain.Products;

namespace Order.Application.Products;

public class ProductsMapper : Profile
{
    public ProductsMapper()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.ProductColors,
                opt => opt.MapFrom(src => src.ProductColors
                    .Select(productColor => productColor.Name)))
            .ForMember(
                dest => dest.ProductSizes,
                opt => opt.MapFrom(src =>
                    src.ProductSizes
                        .Select(productSize => productSize.Name)));
    }
}