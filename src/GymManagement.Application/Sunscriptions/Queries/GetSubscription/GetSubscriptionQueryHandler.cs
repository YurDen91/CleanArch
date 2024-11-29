using GymManagement.Domain.Subscriptions;
using MediatR;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Sunscriptions.Queries.GetSubscription;

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }


    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);
        
        return subscription is null
            ? Error.NotFound(description: "Subscription not found")
            : subscription;
    }
}