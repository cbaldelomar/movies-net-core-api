using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Movies.Api.Middlewares;

namespace Movies.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        // Adds services for using Problem Details format
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);

                if (environment.IsDevelopment())
                {
                    // Add additional details in development environment
                    context.ProblemDetails.Extensions.TryAdd("exception", context.Exception);
                    context.ProblemDetails.Extensions.TryAdd("application", environment.ApplicationName);
                }
            };
        });

        return services;
    }
}
