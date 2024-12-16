using System.Reflection;
using GymManagement.Api.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor,
    IPublisher _publisher)
    : DbContext(options),
        IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Gym> Gyms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public Task CommitChangesAsync()
    {
        // get hold of all domain events
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(domainEvents => domainEvents)
            .ToList();

        // store them in the http context for later if user is waiting online
        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
        }
        else
        {
            domainEvents.ForEach(domainEvent => _publisher.Publish(domainEvent));
        }

        return base.SaveChangesAsync();
    }

    private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // fetch queue from http context or create new queue if it's doesn't exist
        var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
            .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents
            : new Queue<IDomainEvent>();

        // add domain events to the ent of the queue
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // store the queue in the http context
        _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
}