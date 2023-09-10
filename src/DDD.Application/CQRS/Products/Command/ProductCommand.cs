using MediatR;

namespace DDD.Application.CQRS.Products.Command;

public abstract class ProductCommand : IRequest
{
    public string? Name { get; set; }
}
