using DDD.Domain.Entities;
using DDD.Domain.Services.Repositories.Base;

namespace DDD.Domain.Services.Repositories;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
}
