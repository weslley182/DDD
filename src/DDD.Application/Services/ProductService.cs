using AutoMapper;
using DDD.Application.Dtos;
using DDD.Application.Services.Interfaces;
using DDD.Domain.Services.Repositories.Interfaces;

namespace DDD.Application.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repo;
    public ProductService(IMapper mapper, IProductRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
    public List<ProductResponse> GetAllAsync()
    {
        return (List<ProductResponse>)_repo.ToListEntity().ToList().Select(prod => _mapper.Map<List<ProductResponse>>(prod));
    }
}
