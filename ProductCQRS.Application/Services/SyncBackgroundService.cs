using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCQRS.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Services;


public sealed class SyncBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<SyncBackgroundService> _logger;
    private readonly TimeSpan _syncInterval;

    public SyncBackgroundService(
        IServiceProvider services,
        ILogger<SyncBackgroundService> logger,
        IConfiguration config)
    {
        _services = services;
        _logger = logger;
        _syncInterval = config.GetValue<TimeSpan?>("Sync:Interval") ?? TimeSpan.FromMinutes(5);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Sync Background Service started with interval: {Interval}", _syncInterval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _services.CreateScope();
                var syncService = scope.ServiceProvider.GetRequiredService<ISyncService>();

                var result = await syncService.SynchronizeAllAsync(stoppingToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Sync completed with warnings: {Warnings}",
                        string.Join(", ", result.Warnings));
                }
                else
                {
                    _logger.LogDebug("Sync completed successfully. Upserted: {Upserted}, Deleted: {Deleted}",
                        result.UpsertedCount, result.DeletedCount);
                }
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Sync iteration failed");
            }

            await Task.Delay(_syncInterval, stoppingToken);
        }
    }
}
