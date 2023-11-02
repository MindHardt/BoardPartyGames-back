using System.Net;

namespace API;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.Headers.Add("failure-reason", e.Message);
            var statusCode = e switch
            {
                ArgumentException or FormatException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)statusCode;
        }
    }
}

public static class DependencyInjection
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services.AddScoped<ErrorHandlingMiddleware>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<ErrorHandlingMiddleware>();
}