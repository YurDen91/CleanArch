using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Api.Gyms.Queries.GetGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Gym>>;