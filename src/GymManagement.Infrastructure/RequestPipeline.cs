using Microsoft.AspNetCore.Builder;
using GymManagement.Infrastructure.Common.Middleware;

namespace GymManagement.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}