using DDD.Domain.Entities;
using DDD.Domain.Services.Repositories.Interfaces;
using DDD.Infrastructure.Data.Context;
using DDD.Infrastructure.Repositories.Base;

namespace DDD.Infrastructure.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
