using GymManagement.Api.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.Gyms.AddAsync(gym);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
    }

    public Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
        => _dbContext.Gyms
            .Where(gym => gym.SubscriptionId == subscriptionId)
            .ToListAsync();

    public Task RemoveGymAsync(Gym gym)
    {
        _dbContext.Remove(gym);

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<Gym> gyms)
    {
        _dbContext.RemoveRange(gyms);

        return Task.CompletedTask;
    }

    public Task UpdateGymAsync(Gym gym)
    {
        _dbContext.Update(gym);

        return Task.CompletedTask;
    }
}