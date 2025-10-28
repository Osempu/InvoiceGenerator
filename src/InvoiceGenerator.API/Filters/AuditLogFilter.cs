using Microsoft.AspNetCore.Mvc.Filters;

namespace InvoiceGenerator.API.Filters;

public class AuditLogFilter : IActionFilter, IEndpointFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
        var action = context.ActionDescriptor.DisplayName;
        Console.WriteLine($"🕒 [{DateTime.UtcNow}] 🧑🏾 {user} is calling 🎬 {action}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            Console.WriteLine($"🕒 [{DateTime.UtcNow}] ✅ Action executed successfully!.");
        }
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
        var action = context.HttpContext.GetEndpoint()?.DisplayName ?? "Unknown";
        Console.WriteLine($"🕒 [{DateTime.UtcNow}] 🧑🏾 {user} is calling 🎬 {action}");

        var result = await next(context);

        if (context.HttpContext.Response.StatusCode < 400)
        {
            Console.WriteLine($"🕒 [{DateTime.UtcNow}] ✅ Action executed successfully!.");
        }

        return result;
    }
}
