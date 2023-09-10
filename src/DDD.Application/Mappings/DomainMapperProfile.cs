using AutoMapper;

namespace DDD.Application.Mappings;

public class DomainMapperProfile : Profile
{
    public DomainMapperProfile()
    {
        CreateMap<Domain.Entities.Product, Dtos.ProductResponse>().ReverseMap();
        CreateMap<Dtos.ProductRequest, Domain.Entities.Product>().ReverseMap();
    }
}
