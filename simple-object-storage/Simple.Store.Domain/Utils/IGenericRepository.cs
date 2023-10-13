using Microsoft.EntityFrameworkCore;

namespace Simple.Object.Storage.Domain.Utils;

public interface IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    DbSet<TEntity> DbSet { get; }

    IQueryable<TEntity> Tracking { get; }

    IQueryable<TEntity> AsNoTracking { get; }
}