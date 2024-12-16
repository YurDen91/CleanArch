using GymManagement.Domain.Admins;

namespace GymManagement.Api.Common.Interfaces;

public interface IAdminsRepository
{
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
}