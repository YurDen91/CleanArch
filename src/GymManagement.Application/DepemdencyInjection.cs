using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using GymManagement.Api.Common.Behaviors;

namespace GymManagement.Api;

public static class DepemdencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DepemdencyInjection));

            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DepemdencyInjection));

        return services;
    }
}