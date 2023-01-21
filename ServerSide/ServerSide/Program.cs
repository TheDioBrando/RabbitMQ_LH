using ServerSide.Data;
using MassTransit;
using ServerSide.Consumers;
using ServerSide.Mappers;
using ServerSide.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ServerSide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            builder.Services.AddDbContext<LibDbContext>();

            builder.Services.AddTransient<IDbLibraryMapper, DbLibraryMapper>();
            builder.Services.AddTransient<ILibraryRepository, LibraryRepository>();

            builder.Services.AddMassTransit(mt =>
            {
                mt.AddConsumer<CreateLibraryConsumer>();
                mt.AddConsumer<DoesSameLibraryExistConsumer>();
                mt.AddConsumer<UpdateLibraryConsumer>();
                mt.AddConsumer<DeleteLibraryConsumer>();
                mt.AddConsumer<ReadLibraryConsumer>();

                mt.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ReceiveEndpoint("update", ep => ep.ConfigureConsumer<UpdateLibraryConsumer>(context));
                    config.ReceiveEndpoint("create", ep => ep.ConfigureConsumer<CreateLibraryConsumer>(context));
                    config.ReceiveEndpoint("delete", ep => ep.ConfigureConsumer<DeleteLibraryConsumer>(context));
                    config.ReceiveEndpoint("read", ep => ep.ConfigureConsumer<ReadLibraryConsumer>(context));
                    config.ReceiveEndpoint("exist", ep => ep.ConfigureConsumer<DoesSameLibraryExistConsumer>(context));
                }
                );
            });

            builder.Services.AddMassTransitHostedService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            using var serviceScope = app.Services
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<LibDbContext>();

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.Run();
        }
    }
}