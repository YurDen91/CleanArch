using ErrorOr;
using MediatR;
using GymManagement.Domain.Gyms;

namespace GymManagement.Api.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Gym>>>;