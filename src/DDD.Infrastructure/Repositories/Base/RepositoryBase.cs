using DDD.Domain.Base;
using DDD.Domain.Services.Repositories.Base;
using DDD.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public IQueryable<TEntity> ListBy(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return ToListEntity(includeProperties).Where(where);
    }

    public IQueryable<TEntity> ListAndOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return asc ? ListBy(where, includeProperties).OrderBy(order) : ListBy(where, includeProperties).OrderByDescending(order);
    }

    public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await ToListEntity(includeProperties).FirstOrDefaultAsync(where) ?? throw new EntityNotFoundException<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        if (includeProperties.Any())
        {
            return await ToListEntity(includeProperties).FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString()) ?? throw new EntityNotFoundException<TEntity>();
        }

        return await _context.Set<TEntity>().FindAsync(id) ?? throw new EntityNotFoundException<TEntity>();
    }

    public IQueryable<TEntity> ToListEntity(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (includeProperties.Any())
        {
            return Include(_context.Set<TEntity>(), includeProperties);
        }

        return query;
    }

    public IQueryable<TEntity> ToListOrderBy<TKey>(Expression<Func<TEntity, TKey>> order, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return asc ? ToListEntity(includeProperties).OrderBy(order) : ToListEntity(includeProperties).OrderByDescending(order);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        return entity;
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    /// <summary>
    /// Adicionar um coleção de entidades ao contexto do entity framework
    /// </summary>
    /// <param name="entities">Lista de entidades que deverão ser persistidas</param>
    /// <returns></returns>
    public async Task AddListAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    /// <summary>
    /// Verifica se existe algum objeto com a condição informada
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public bool Exists(Func<TEntity, bool> where)
    {
        return _context.Set<TEntity>().Any(where);
    }

    /// <summary>
    /// Realiza include populando o objeto passado por parametro
    /// </summary>
    /// <param name="query">Informe o objeto do tipo IQuerable</param>
    /// <param name="includeProperties">Ínforme o array de expressões que deseja incluir</param>
    /// <returns></returns>
    private IQueryable<TEntity> Include(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        foreach (var property in includeProperties)
        {
            query = query.Include(property);
        }

        return query;
    }
}
