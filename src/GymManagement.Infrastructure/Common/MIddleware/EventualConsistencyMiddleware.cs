﻿using GymManagement.Domain.Common;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymManagementDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
                    value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // notify the user that something went wrong, even they got a good response,
                // due to an unexpected error
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}