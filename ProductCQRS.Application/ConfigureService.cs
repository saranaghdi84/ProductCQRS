using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Application.Services;
using ProductCQRS.Application.UseCases.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            // Mapster configuration
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper>();

            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<SyncResult>();
            services.AddScoped<Result>();
            services.AddHostedService<SyncBackgroundService>();
            services.AddScoped<ISyncService, SyncService>();
            return services;
        }
    }
}
