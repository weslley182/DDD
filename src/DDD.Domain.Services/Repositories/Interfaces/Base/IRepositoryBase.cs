namespace DDD.Domain.Services.Repositories.Interfaces.Base;

public interface IRepositoryBase<TEntity, TId>
      where TEntity : class
      where TId : struct
{
    Task<TEntity> GetByIdAsync(TId id);

    Task AddAsync(TEntity entity);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();
}
