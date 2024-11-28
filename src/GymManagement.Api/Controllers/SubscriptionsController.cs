using GymManagement.Application.Sunscriptions.Commands.CreateSubscriptionFolder;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ILogger<SubscriptionsController> _logger;
    private readonly ISender _mediator;

    public SubscriptionsController(
        ILogger<SubscriptionsController> logger,
        ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        var command = new CreateSubscriptionCommand(
            request.SubscriptionType.ToString(),
            request.AdminId);

        var subscriptionId = await _mediator.Send(command);

        var response = new SubscriptionResponse(
            subscriptionId,
            request.SubscriptionType);

        return Ok(response);

    }

    [HttpGet]
    public IEnumerable<SubscriptionResponse> Get()
    {
        return new List<SubscriptionResponse>
        {
            new SubscriptionResponse(Guid.NewGuid(), SubscriptionType.Free),
            new SubscriptionResponse(Guid.NewGuid(), SubscriptionType.Starter),
            new SubscriptionResponse(Guid.NewGuid(), SubscriptionType.Pro)
        };
    }
}