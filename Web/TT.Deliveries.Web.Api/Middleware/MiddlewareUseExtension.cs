using Microsoft.AspNetCore.Builder;

namespace TT.Deliveries.Web.Api.Middleware
{
    public static class MiddlewareUseExtension
    {
        public static IApplicationBuilder UseMiddlewares(
            this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestLoggingMiddleware>();
            builder.UseMiddleware<ExceptionHandlingMiddleware>();
            return builder;
        }
    }
}
