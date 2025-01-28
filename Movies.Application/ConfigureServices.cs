using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Behaviors;

namespace Movies.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;

        // MediatR
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // includeInternalTypes: true => when the level access of your validators is internal
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
