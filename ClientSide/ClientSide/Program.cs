using Broker.Requests;
using BrokerRequests;
using ClientSide.Commands.Libraries;
using ClientSide.Validation.Libraries;
using MassTransit;

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
            builder.Services.AddTransient<ICreateLibraryCommand, CreateLibraryCommand>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}