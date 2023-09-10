﻿using DDD.Application.CQRS.Products.Queries;
using DDD.Domain.Entities;
using DDD.Domain.Services.Repositories.Interfaces;
using MediatR;

namespace DDD.Application.CQRS.Products.Handlers;

public class ProductAllQueryHandler : IRequestHandler<ProductAllQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repo;
    public ProductAllQueryHandler(IProductRepository repo)
    {
        _repo = repo;
    }
    public Task<IEnumerable<Product>> Handle(ProductAllQuery request, CancellationToken cancellationToken)
    {
        return (Task<IEnumerable<Product>>)_repo.ToListEntity().ToList().Select(prod => prod);
    }
}
