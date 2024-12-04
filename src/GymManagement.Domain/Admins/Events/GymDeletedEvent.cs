using GymManagement.Domain.Common;

namespace GymManagement.Domain.Admins.Events
{
    internal class GymDeletedEvent(Guid gymId) : IDomainEvent;
}
