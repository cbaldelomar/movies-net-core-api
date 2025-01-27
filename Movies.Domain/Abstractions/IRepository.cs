using System.Linq.Expressions;

namespace Movies.Domain.Abstractions;

public interface IRepository<TEntity, TEntityId>
    where TEntity : AggregateRoot<TEntityId>
{
    Task<TEntity?> GetByIdAsync(
        TEntityId id,
        params Expression<Func<TEntity, object>>[] includes);

    Task<IEnumerable<TEntity>> GetListAsync(
        params Expression<Func<TEntity, object>>[] includes);

    Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes);

    void Add(TEntity entity);

    void Remove(TEntity entity);
}
