using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins.Persistence;

public class AdminsRepository(GymManagementDbContext dbContext) : IAdminsRepository
{
    public async Task AddAdminAsync(Admin admin)
    {
        await dbContext.Admins.AddAsync(admin);
    }

    public Task<Admin?> GetByIdAsync(Guid adminId)
        => dbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);

    public Task UpdateAsync(Admin admin)
    {
        dbContext.Admins.Update(admin);

        return Task.CompletedTask;
    }
}