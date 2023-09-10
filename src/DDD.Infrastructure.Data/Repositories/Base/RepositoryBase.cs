using DDD.Domain.Base;
using DDD.Domain.Services.Repositories.Interfaces.Base;
using DDD.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Repositories.Base;

public class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : EntityBase
        where TId : struct
{
    private readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id) ?? throw new EntityNotFoundException<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public TEntity Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);

        return entity;
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        return await query.ToListAsync();
    }
}
