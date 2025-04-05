using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.Contracts;

public interface ISyncService
{
    Task<SyncResult> SynchronizeAllAsync(CancellationToken ct = default);
}
