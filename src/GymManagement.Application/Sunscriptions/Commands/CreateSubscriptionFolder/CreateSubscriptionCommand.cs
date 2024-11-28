using MediatR;

namespace GymManagement.Application.Sunscriptions.Commands.CreateSubscriptionFolder;

public record CreateSubscriptionCommand(string subscriptionType, Guid adminId) : IRequest<Guid>;
