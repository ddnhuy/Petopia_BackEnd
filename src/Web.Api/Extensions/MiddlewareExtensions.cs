using Web.Api.Authorization;
using Web.Api.Middleware;

namespace Web.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        app.UseMiddleware<AuthorizationMiddleware>();

        return app;
    }
}
