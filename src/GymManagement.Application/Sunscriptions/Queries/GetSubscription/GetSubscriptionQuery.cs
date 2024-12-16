using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Api.Sunscriptions.Queries.GetSubscription;

public record GetSubscriptionQuery(Guid SubscriptionId) : IRequest<ErrorOr<Subscription>>;
