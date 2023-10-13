using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Simple.Object.Storage.Domain.Entities.ObjectStorage;
using Simple.Object.Storage.Infrastructure.Options;

namespace Simple.Object.Storage.Infrastructure.Entities.ObjectStorage;

public class ObjectStorageRepository : IObjectStorageRepository
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<ObjectStorageRepository> _logger;
    private readonly MinIOConfiguration _minIoConfiguration;

    public ObjectStorageRepository(IMinioClient minioClient, IOptions<MinIOConfiguration> options,
        ILogger<ObjectStorageRepository> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
        _minIoConfiguration = options.Value;
    }

    public async Task<bool> PutObjectAsync(CancellationToken cancellationToken)
    {
        return await AddIfNewBucket(_minIoConfiguration.BucketName, cancellationToken);
    }

    private async Task<bool> AddIfNewBucket(string bucketName, CancellationToken cancellationToken)
    {
        try
        {
            var d = new BucketExistsArgs().WithBucket(bucketName);
            d.IsBucketCreationRequest = true;


            var found = await _minioClient.BucketExistsAsync(d,
                cancellationToken);
            if (!found)
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName), cancellationToken);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}