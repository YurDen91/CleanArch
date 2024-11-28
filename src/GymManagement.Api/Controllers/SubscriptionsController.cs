using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ILogger<SubscriptionsController> _logger;

    public SubscriptionsController(ILogger<SubscriptionsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateSubscription(CreateSubscriptionRequest request)
    {
        return Ok(request);
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