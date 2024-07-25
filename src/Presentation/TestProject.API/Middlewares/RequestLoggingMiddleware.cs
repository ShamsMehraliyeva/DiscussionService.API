using System.Text;
using Application.CrossCuttingConcers.Logging;

namespace TestProject.API.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, ILoggerService _logger)
    {
        await OnBefore(context, _logger);
        await _next(context);
        await OnAfter(context, _logger);
    }
    private async Task OnBefore(HttpContext context, ILoggerService _logger)
    {

        context.Request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
        await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
        var body = Encoding.UTF8.GetString(buffer);

        context.Request.Body.Position = 0;

        LogModel logModel = new()
        {
            Method = context.Request.Method,
            Url = $"{context.Request.Path}{context.Request.QueryString}",
            Body = body,
            Step = "Controller",
            Message = "Endpoint on before log.",
        };

        _logger.Information(logModel);
    }
    private async Task OnAfter(HttpContext context, ILoggerService _logger)
    {

        context.Request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
        await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
        var body = Encoding.UTF8.GetString(buffer);

        context.Request.Body.Position = 0;


        LogModel logModel = new()
        {
            Method = context.Request.Method,
            Url = $"{context.Request.Path}{context.Request.QueryString}",
            Body = body,
            Step = "Controller",
            Message = "Endpoint on after log.",
        };

        _logger.Information(logModel);
    }
}