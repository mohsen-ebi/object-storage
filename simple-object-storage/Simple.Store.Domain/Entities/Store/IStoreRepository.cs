using Simple.Object.Storage.Domain.Utils;

namespace Simple.Object.Storage.Domain.Entities;

public interface IStoreRepository : IGenericRepository<Store, long>
{
    Task<long> Add(Store store, CancellationToken cancellationToken);
}