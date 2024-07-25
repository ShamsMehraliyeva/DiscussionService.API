using System.Text;
using Application.CrossCuttingConcers.Logging;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Pipelines;

public class LoggingBehaviour<TRequest, TResponse> : BaseBehaviorInterceptor<TRequest, TResponse>, IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
     private readonly ILoggerService _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    public LoggingBehaviour(ILoggerService logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IAuthService authService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _authService = authService;
    }
    
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        => Invoke(next, request, cancellationToken);
    protected override async Task OnBefore(TRequest request, CancellationToken cancellationToken)
    {
        HttpContext context = _httpContextAccessor.HttpContext;

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
            Step = "Mediator",
            Message = $"Entered in '{request.GetType().Name}' handler."
        };

        _logger.Information(logModel);
    }

    protected override async Task OnAfter(TRequest request, CancellationToken cancellationToken)
    {
        HttpContext context = _httpContextAccessor.HttpContext;

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
            Step = "Mediator",
            Message = $"Exited from '{request.GetType().Name}' handler."
        };

        _logger.Information(logModel);
    }


}