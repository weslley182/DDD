using AutoMapper;
using DDD.Application.CQRS.Products.Command;
using DDD.Domain.Entities;
using DDD.Domain.Services.Repositories.Interfaces;
using MediatR;

namespace DDD.Application.CQRS.Products.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommad>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public ProductCreateCommandHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task Handle(ProductCreateCommad request, CancellationToken cancellationToken)
    {
        var prod = _mapper.Map<Product>(request);
        await _repo.AddAsync(prod);
    }
}
