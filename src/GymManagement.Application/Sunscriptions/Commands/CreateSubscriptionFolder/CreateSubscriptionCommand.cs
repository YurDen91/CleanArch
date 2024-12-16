using MediatR;
using ErrorOr;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Api.Sunscriptions.Commands.CreateSubscriptionFolder;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;
