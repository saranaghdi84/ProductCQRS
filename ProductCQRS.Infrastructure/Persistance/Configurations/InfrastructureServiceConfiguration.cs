using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.Services;
using ProductCQRS.Infrastructure.Persistance.Context;
using ProductCQRS.Infrastructure.Persistance.Repositories;
using ProductCQRS.Infrastructure.Persistance.UnitOfWorks;
using ProductCQRS.Infrastructure.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Infrastructure.Persistance.Configurations;

public static class InfrastructureServiceConfiguration
{


    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<CommandDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CommandDbConnection"),
                sqlOpts => sqlOpts.EnableRetryOnFailure()));

        services.AddDbContext<QueryDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("QueryDbConnection"),
                sqlOpts => sqlOpts.EnableRetryOnFailure()));


        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductCategoryReadRepository, ProductCategoryReadRepository>();



        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReadUnitOfWork, ReadUnitOfWork>();


        services.AddScoped<IDatabaseSynchronizer, DatabaseSynchronizer>();




        return services;
    }




    public static async Task InitializeDatabasesAsync(
      this IServiceProvider services,
      ILogger logger)  
    {
        try
        {

            await services.GetRequiredService<CommandDbContext>().Database.MigrateAsync();
            await services.GetRequiredService<QueryDbContext>().Database.MigrateAsync();


            var result = await services.GetRequiredService<IDatabaseSynchronizer>().FullSyncAsync();

            if (!result.IsSuccess)
                logger.LogError("Initial sync failed: {Error}", result.Error);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database initialization failed");
            throw;
        }
    }
}

