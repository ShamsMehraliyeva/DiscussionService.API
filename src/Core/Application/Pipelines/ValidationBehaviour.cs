using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Pipelines;

public class ValidationBehaviour<TRequest, TResponse> : BaseBehaviorInterceptor<TRequest, TResponse>, IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;



    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, IServiceProvider provider) => _validators = validators;


    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        => Invoke(next, request, cancellationToken);


    protected override async Task OnBefore(TRequest request, CancellationToken cancellationToken)
    {
        if (_validators.Any())
            await Validate(request, cancellationToken);
    }

    private async Task Validate(TRequest request, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        Dictionary<string, string> errors = failures.ToDictionary(p => p.PropertyName, p => p.ErrorMessage);

        if (failures.Any())
            throw new ValidationException(failures);
    }


}