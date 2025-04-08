using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Application.ResultHandler.LocalizationResources;
using ProductCQRS.Application.Services;
using System.Reflection;

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
            services.AddScoped<IMapper, Mapper>();
            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<LocalizedMessages>();
            services.AddScoped<SyncResult>();
            //services.AddScoped<Result>();
            services.AddFluentValidationAutoValidation();
            services.AddHostedService<SyncBackgroundService>();
            services.AddScoped<ISyncService, SyncService>();
            return services;
        }
    }
}
