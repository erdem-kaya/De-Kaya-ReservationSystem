using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction? _transaction = null!;

    #region TransactionManagment
    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    #endregion

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return null!;

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} entity : {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        try
        {
            IQueryable<TEntity> entities = _dbSet;
            if (expression != null)
                entities = entities.Where(expression);
            var result = await entities.ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all {nameof(TEntity)} entities : {ex.Message}");
            return null!;
        }
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try 
        { 
            var result = await _dbSet.FirstOrDefaultAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting {nameof(TEntity)} entity : {ex.Message}");
            return null!;
        }
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity updatedEntity)
    {
        if (id <= 0 || updatedEntity == null)
            return null!;

        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return null!;

            _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating {nameof(TEntity)} entity : {ex.Message}");
            return null!;
        }
    }




    public async Task<bool> DeleteAsyncById(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} entity : {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var result = await _dbSet.AnyAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error checking if {nameof(TEntity)} entity exists : {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        try
        {

            var result = await _dbSet.FindAsync(id) != null;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error checking if {nameof(TEntity)} entity exists : {ex.Message}");
            return false;
        }
    }
   
}
