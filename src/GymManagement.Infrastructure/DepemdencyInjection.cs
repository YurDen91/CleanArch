using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Common.Persistance;
using GymManagement.Infrastructure.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DepemdencyInjection
{
    public static IServiceCollection AddInfrastrucuture(this IServiceCollection services)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GymManagementDbContext>();
        optionsBuilder.UseSqlite("Data Source=GymManagement.db");
        
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());
        return services;
    }
}