using MediatR;
using ErrorOr;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Sunscriptions.Commands.CreateSubscriptionFolder;

public record CreateSubscriptionCommand(SubscriptionType subscriptionType, Guid adminId) : IRequest<ErrorOr<Subscription>>;
