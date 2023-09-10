using DDD.Domain.Entities;
using DDD.Domain.Services.Repositories.Interfaces.Base;

namespace DDD.Domain.Services.Repositories.Interfaces;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
}
