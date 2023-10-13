using Simple.Object.Storage.Domain.Entities;
using Simple.Object.Storage.Infrastructure.Utils;

namespace Simple.Object.Storage.Infrastructure.Entities.Store;

public class StoreRepository : GenericRepository<Domain.Entities.Store, long, StoreContext>,IStoreRepository
{
    private readonly StoreContext _dbContext;

    public StoreRepository(StoreContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async  Task<long> Add(Domain.Entities.Store store, CancellationToken cancellationToken)
    {
        try
        {
            await DbSet.AddAsync(store, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return store.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}