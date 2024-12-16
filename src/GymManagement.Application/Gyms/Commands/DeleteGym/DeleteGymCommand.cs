using ErrorOr;
using MediatR;

namespace GymManagement.Api.Gyms.Commands.DeleteGym;

public record DeleteGymCommand(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Deleted>>;