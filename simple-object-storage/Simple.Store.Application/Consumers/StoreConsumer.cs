using MassTransit;
using Microsoft.Extensions.Logging;
using Simple.Object.Storage.Application.Contracts.Requests;
using Simple.Object.Storage.Domain.Entities.ObjectStorage;

namespace Simple.Object.Storage.Application.Consumers;

public class StoreConsumer : IConsumer<StoreRequestContract>
{
    private readonly ILogger<StoreConsumer> _logger;
    private readonly IObjectStorageRepository _objectStorageRepository;

    public StoreConsumer(ILogger<StoreConsumer> logger, IObjectStorageRepository objectStorageRepository)
    {
        _logger = logger;
        _objectStorageRepository = objectStorageRepository;
    }

    public async Task Consume(ConsumeContext<StoreRequestContract> context)
    {
        try
        {
            var request = context.Message;
            var cancellationToken = context.CancellationToken;
            await _objectStorageRepository.PutObjectAsync(cancellationToken: cancellationToken);
            await context.ConsumeCompleted;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}