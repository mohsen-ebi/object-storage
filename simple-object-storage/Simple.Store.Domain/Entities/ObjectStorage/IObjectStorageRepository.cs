namespace Simple.Object.Storage.Domain.Entities.ObjectStorage;

public interface IObjectStorageRepository
{
    Task<bool> PutObjectAsync(CancellationToken cancellationToken);
}