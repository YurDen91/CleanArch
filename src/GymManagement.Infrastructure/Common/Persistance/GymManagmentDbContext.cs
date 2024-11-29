using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistance;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Gym> Gyms { get; set; } = null!;
    
    public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public Task CommitChangesAsync()
        => base.SaveChangesAsync();
}