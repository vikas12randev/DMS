using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TT.Deliveries.Web.Api.Middleware;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        _logger.LogInformation("Requested Path: {Path}, Method: {Method}", context.Request.Path, context.Request.Method);

        await _next(context);

        watch.Stop();

        _logger.LogInformation("Response StatusCode: {ResponseStatusCode}", context.Response.StatusCode);
        _logger.LogInformation("Response ResponseTimeMilliseconds: {ResponseTimeMilliseconds} ms", watch.ElapsedMilliseconds);
    }
}