using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

}
