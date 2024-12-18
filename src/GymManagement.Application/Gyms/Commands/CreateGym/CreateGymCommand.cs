using ErrorOr;
using GymManagement.Api.Common.Authorization;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Api.Gyms.Commands.CreateGym;

[Authorize(Roles = "Admin")]
public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;