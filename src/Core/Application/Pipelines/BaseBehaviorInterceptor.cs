using MediatR;

namespace Application.Pipelines;

public class BaseBehaviorInterceptor<TRequest, TResponse>
{
    protected TRequest Request;

    protected async Task<TResponse> Invoke(RequestHandlerDelegate<TResponse> next, TRequest request, CancellationToken cancellationToken)
    {
        Request = request;
        await OnBefore(request, cancellationToken);
        Task<TResponse> response = next();
        await OnAfter(request, cancellationToken);
        return await response;
    }

    protected virtual async Task OnBefore(TRequest request, CancellationToken cancellationToken) => await Task.CompletedTask;

    protected virtual async Task OnAfter(TRequest request, CancellationToken cancellationToken) => await Task.CompletedTask;

}