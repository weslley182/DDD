using AutoMapper;
using DDD.Application.CQRS.Products.Command;
using DDD.Application.CQRS.Products.Queries;
using DDD.Application.Dtos;
using DDD.Application.UseCase.Interface;
using MediatR;

namespace DDD.Application.UseCase;

public class ProductUseCase : IProductUseCase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductUseCase(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    public Task AddProductAsync(ProductRequest request)
    {
        var command = new ProductCreateCommad() { Name = request.Name };
        _mediator.Send(command);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<ProductResponse>> GetAllProductsAsyc()
    {
        var query = new ProductGetAllQuery();
        var prod = _mediator.Send(query);
        return (Task<IEnumerable<ProductResponse>>)_mapper.Map<IEnumerable<ProductResponse>>(prod);
    }
}
