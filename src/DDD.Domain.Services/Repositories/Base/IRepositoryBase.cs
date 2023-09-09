using System.Linq.Expressions;

namespace DDD.Domain.Services.Repositories.Base;

public interface IRepositoryBase<TEntity, TId>
      where TEntity : class
      where TId : struct
{
    IQueryable<TEntity> ListBy(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> ListAndOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> ordem, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

    bool Exists(Func<TEntity, bool> where);

    IQueryable<TEntity> ToListEntity(params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> ToListOrderBy<TKey>(Expression<Func<TEntity, TKey>> order, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties);

    Task AddAsync(TEntity entity);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity);

    Task AddListAsync(IEnumerable<TEntity> entities);
}
