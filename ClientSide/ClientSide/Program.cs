using Broker.Requests;
using BrokerRequests;
using ClientSide.Commands.Libraries;
using ClientSide.Validation.Libraries;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ServerSide.Commands.Libraries;

namespace ClientSide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddTransient<ICreateLibraryRequestValidator, CreateLibraryRequestValidator>();
            builder.Services.AddTransient<IUpdateLibraryRequestValidator, UpdateLibraryRequestValidator>();
            builder.Services.AddTransient<IDeleteLibraryRequestValidator, DeleteLibraryRequestValidator>();
            builder.Services.AddTransient<ICreateLibraryCommand, CreateLibraryCommand>();
            builder.Services.AddTransient<IUpdateLibraryCommand, UpdateLibraryCommand>();
            builder.Services.AddTransient<IDeleteLibraryCommand, DeleteLibraryCommand>();
            builder.Services.AddTransient<IReadLibraryCommand, ReadLibraryCommand>();

            builder.Services.AddMassTransit( mt =>
            {
                mt.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                });

                mt.AddRequestClient<PostUserRequest>(new Uri("rabbitmq://localhost/post"));
                mt.AddRequestClient<CreateLibraryRequest>(new Uri("rabbitmq://localhost/create"));
                mt.AddRequestClient<UpdateLibraryRequest>(new Uri("rabbitmq://localhost/update"));
                mt.AddRequestClient<DeleteLibraryRequest>(new Uri("rabbitmq://localhost/delete"));
                mt.AddRequestClient<ReadLibraryRequest>(new Uri("rabbitmq://localhost/read"));
                mt.AddRequestClient<DoesSameLibraryExistRequest>(new Uri("rabbitmq://localhost/exist"));
            });

            builder.Services.AddMassTransitHostedService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}