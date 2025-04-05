using Microsoft.Extensions.Logging;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.Services;


public class SyncService : ISyncService
{
    private readonly IDatabaseSynchronizer _synchronizer;
    private readonly ILogger<SyncService> _logger;

    public SyncService(
        IDatabaseSynchronizer synchronizer,
        ILogger<SyncService> logger)
    {
        _synchronizer = synchronizer;
        _logger = logger;
    }

    public async Task<SyncResult> SynchronizeAllAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Starting database synchronization");
        var result = await _synchronizer.FullSyncAsync(ct);

        if (result.IsSuccess)
        {
            _logger.LogInformation(
                "Sync completed: {Upserted} upserted, {Deleted} deleted",
                result.UpsertedCount,
                result.DeletedCount);
        }
        else
        {
            _logger.LogError("Sync failed: {Error}", result.Error);
        }

        return result;
    }
}
