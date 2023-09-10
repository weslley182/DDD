using DDD.Domain.Entities;
using MediatR;

namespace DDD.Application.CQRS.Products.Queries;

public class ProductAllQuery : IRequest<IEnumerable<Product>>
{
    public int Id { get; set; }
}
