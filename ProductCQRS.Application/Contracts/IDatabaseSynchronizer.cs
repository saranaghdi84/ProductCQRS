using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.Contracts;


public interface IDatabaseSynchronizer
{
    Task<SyncResult> SyncProductsAsync(CancellationToken ct = default);
    Task<SyncResult> SyncCategoriesAsync(CancellationToken ct = default);
    Task<SyncResult> FullSyncAsync(CancellationToken ct = default);
}
