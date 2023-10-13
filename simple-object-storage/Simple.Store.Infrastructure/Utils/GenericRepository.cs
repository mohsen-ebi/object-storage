using Simple.Object.Storage.Domain;
using Simple.Object.Storage.Domain.Utils;

namespace Simple.Object.Storage.Infrastructure.Utils;

public class GenericRepository<TEntity, TKey, TDbContext> : IGenericRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected readonly TDbContext DbContext;

    public DbSet<TEntity> DbSet { get; }

    public virtual IQueryable<TEntity> Tracking => (IQueryable<TEntity>)this.DbSet;

    public virtual IQueryable<TEntity> AsNoTracking => this.DbSet.AsNoTracking<TEntity>();

    public GenericRepository(TDbContext dbContext)
    {
        this.DbContext = dbContext;
        this.DbSet = this.DbContext.Set<TEntity>();
    }
}