namespace TestProject.API.Middlewares;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}