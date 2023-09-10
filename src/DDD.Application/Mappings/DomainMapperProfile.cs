using AutoMapper;
using DDD.Application.CQRS.Products.Command;
using DDD.Application.Dtos;
using DDD.Domain.Entities;

namespace DDD.Application.Mappings;

public class DomainMapperProfile : Profile
{
    public DomainMapperProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<ProductRequest, Product>().ReverseMap();
        CreateMap<ProductCreateCommad, Product>().ReverseMap();
    }
}
