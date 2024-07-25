using System.Net;
using Application.CrossCuttingConcers.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ValidationProblemDetails = Application.CrossCuttingConcers.Exceptions.ValidationProblemDetails;

namespace TestProject.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AggregateException aggregateException)
        {
            foreach (var innerException in aggregateException.InnerExceptions)
            {
                await HandleExceptionAsync(context, innerException);
            }
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        string a = exception.GetType().ToString();
        if (exception.GetType() == typeof(ValidationException)) return CreateValidationException(context, exception);
        if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
        if (exception.GetType() == typeof(AuthorizationException))
            return CreateAuthorizationException(context, exception);
        return CreateInternalException(context, exception);
    }
    private Task CreateAuthorizationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

        return context.Response.WriteAsync(new AuthorizationProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://example.com/probs/authorization",
            Title = "Authorization exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        object errors = ((ValidationException)exception).Errors;

        return context.Response.WriteAsync(new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        }.ToString());
    }


    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

        return context.Response.WriteAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }
}