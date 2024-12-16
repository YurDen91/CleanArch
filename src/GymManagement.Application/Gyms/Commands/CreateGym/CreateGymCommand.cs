using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Api.Gyms.Commands.CreateGym;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;