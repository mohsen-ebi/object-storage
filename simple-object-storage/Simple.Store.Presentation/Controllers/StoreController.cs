using System.Net;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Simple.Object.Storage.Application.Contracts.Requests;
using Simple.Object.Storage.Application.Contracts.Responses;
using Simple.Object.Storage.Application.Utils;

namespace Simple.Object.Storage.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<StoreController> _logger;

    public StoreController(ILogger<StoreController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "upload")]
    public async Task<IActionResult> Upload([FromForm] StoreRequestContract request,
        CancellationToken cancellationToken)
    {
        var requestClient = _mediator.CreateRequestClient<StoreRequestContract>();
        var (accepted, rejected) =
            await requestClient.GetResponse<StoreResponseContract, ConsumerRejected>(request, cancellationToken);
        //todo we can put this to genericClass to handle response and errors in general mode also log result into log service
        if (accepted.IsCompletedSuccessfully)
        {
        }
        else
        {
        }

        return new BadRequestResult();
    }

    [HttpGet("test")]
    [ProducesResponseType(typeof(StoreTestResponseContract), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TestGet([FromQuery] StoreTestRequestContract request,
        CancellationToken cancellationToken)
    {
        var requestClient = _mediator.CreateRequestClient<StoreTestRequestContract>();
        var (accepted, rejected) =
            await requestClient.GetResponse<StoreTestResponseContract, ConsumerRejected>(request,
                cancellationToken: cancellationToken);

        //todo we can put this to genericClass to handle response and errors in general mode also log result into log service
        if (accepted.IsCompletedSuccessfully)
        {
            var result = accepted.Result.Message;
            return new OkObjectResult(result);
        }
        else
        {
        }

        return new BadRequestResult();
    }
}