using DDD.Domain.Entities;
using MediatR;

namespace DDD.Application.CQRS.Products.Queries;

public class ProductGetAllQuery : IRequest<IEnumerable<Product>>
{
}
