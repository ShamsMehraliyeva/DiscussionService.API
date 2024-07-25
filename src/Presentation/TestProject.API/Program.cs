using Persistence;
using Application;
using Application.Pipelines;
using MediatR;
using TestProject.API.Middlewares;

namespace TestProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ConfigureMiddlewares();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            
            app.Run();
        }
    }
}
