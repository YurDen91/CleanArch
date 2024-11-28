using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DepemdencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(otions =>
        {
            otions.RegisterServicesFromAssemblyContaining(typeof(DepemdencyInjection));
        });

        return services;
    }
}