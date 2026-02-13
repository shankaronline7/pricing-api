using System.Linq.Expressions;


namespace Pricing.Application.Common.Interfaces.Data
{
    public interface IRepositoryBase<TEntity> : IDisposable
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);

        Task<TEntity?> FindIEAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllIEAsync();
        Task AddRangeIEAsync(List<TEntity> entities);
    }

}
