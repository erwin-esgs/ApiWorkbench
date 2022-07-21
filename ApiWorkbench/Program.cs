using MediatR;
using System.Reflection;
using FluentValidation;
using ApiWorkbench.Properties;

namespace ApiWorkbench
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(o => o.AllowEmptyInputInBodyModelBinding = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            
            builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
            //builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EmployeeAddBehavior<,>));

            //builder.Services.AddMediatR(typeof(EmployeeAddHandler));
            //builder.Services.Register(typeof(IPipelineBehavior<,>), Assembly.GetExecutingAssembly());
            //builder.Services.AddScoped(typeof(IPipelineBehavior<EmployeeAddRequest, EmployeeAddResponse>));
            //builder.Services.AddSingleton<IValidator<EmployeeAddRequest>, EmployeeAddRequestValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseWebSockets();
            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };
            app.UseWebSockets(webSocketOptions);
            /*
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        Console.WriteLine(webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    await next(context);
                }

            });
            */
            

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
