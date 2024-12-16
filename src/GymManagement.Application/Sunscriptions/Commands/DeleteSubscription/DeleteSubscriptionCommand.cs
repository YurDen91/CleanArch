using ErrorOr;
using MediatR;

namespace GymManagement.Api.Subscriptions.Commands.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid SubscriptionId) : IRequest<ErrorOr<Deleted>>;