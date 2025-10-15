using System;
using Microsoft.AspNetCore.Diagnostics;

namespace InvoiceGenerator.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext context) => {
            Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if(exception is null) {
                //Handle this unexpected case
                return Results.Problem();
            }
            //Custom global error handling
            return Results.Problem();
        });

        app.UseStatusCodePages();

        return app;
    }

    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
