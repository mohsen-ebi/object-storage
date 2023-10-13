using MassTransit;
using Microsoft.Extensions.Logging;
using Simple.Object.Storage.Application.Contracts.Requests;
using Simple.Object.Storage.Application.Contracts.Responses;
using Simple.Object.Storage.Application.Utils;

namespace Simple.Object.Storage.Application.Consumers;

public class StoreTestConsumer : IConsumer<StoreTestRequestContract>
{
    private readonly ILogger<StoreTestConsumer> _logger;

    public StoreTestConsumer(ILogger<StoreTestConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<StoreTestRequestContract> context)
    {
        try
        {
            var request = context.Message;
            var cancellationToken = context.CancellationToken;
            await context.RespondAsync(new StoreTestResponseContract()
            {
                Result = $"the id is :{request.Id}"
            });
        }
        catch (Exception e)
        {
            await context.RespondAsync<ConsumerRejected>(new ConsumerRejected()
            {
                StatusCode = ConsumerStatusCode.BadRequest,
                Reason = e.Message
            });
        }
    }
}