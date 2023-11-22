using wrssolutions.Data.Entity;
using wrssolutions.Domain.MongoEntities.LoggerApplication;
using wrssolutions.Services.Interfaces;
using wrssolutions.Services.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace wrssolutions.IoC.ServicesDepencency
{
    public static class ServicesDepencency
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            //Cors
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    );
            });

            //Services
            services.AddScoped<ISvcAuthJwtToken, SvcAuthJwtToken>();
            services.AddScoped<ISvcPerson, SvcPerson>();
            services.AddScoped<ISvcClientCompany, SvcClientCompany>();
            services.AddScoped<ISvcLoggerMongo, SvcLoggerMongo>();
            services.AddScoped<ISvcRabbitMQ, SvcRabbitMQ>(_provider => new SvcRabbitMQ(_provider.GetService<ISvcLoggerMongo>()!, configuration["RabbitMQSettings:HostName"]!));

            //services.AddSingleton<IfncApi, fncApi>();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogger>>();
            //Add Singleton if you want to use Generic class logger in place of ILogger<T>
            services.AddSingleton(typeof(ILogger), logger!);
        }
    }
}
