using System;
using System.Diagnostics;
using InvoiceGenerator.API.CustomExceptions;
using Microsoft.AspNetCore.Http.Features;

namespace InvoiceGenerator.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomErrorHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options => {
            
            options.CustomizeProblemDetails = context => {
                
                context.ProblemDetails.Instance = 
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        return services;
    }
}
