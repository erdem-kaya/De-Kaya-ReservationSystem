using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(int id, TEntity updatedEntity);
    Task<bool> DeleteAsyncById(int id);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsByIdAsync(int id);

}
