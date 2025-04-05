using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Infrastructure.Persistance.Context;

namespace ProductCQRS.Infrastructure.Sync;

public sealed class DatabaseSynchronizer : IDatabaseSynchronizer
{
    private readonly CommandDbContext _commandDb;
    private readonly QueryDbContext _queryDb;
    private readonly ILogger<DatabaseSynchronizer> _logger;

    public DatabaseSynchronizer(
        CommandDbContext commandDb,
        QueryDbContext queryDb,
        ILogger<DatabaseSynchronizer> logger)
    {
        _commandDb = commandDb;
        _queryDb = queryDb;
        _logger = logger;
    }

    public async Task<SyncResult> FullSyncAsync(CancellationToken ct = default)
    {
        var result = new SyncResult();
        await using var transaction = await _queryDb.Database.BeginTransactionAsync(ct);

        try
        {
            var productsResult = await SyncProductsAsync(ct);
            var categoriesResult = await SyncCategoriesAsync(ct);

            if (!productsResult.IsSuccess || !categoriesResult.IsSuccess)
            {
                await transaction.RollbackAsync(ct);
                return SyncResult.Failure("Partial synchronization failure");
            }

            await _queryDb.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return SyncResult.Success(
                productsResult.UpsertedCount + categoriesResult.UpsertedCount,
                productsResult.DeletedCount + categoriesResult.DeletedCount
            ).WithWarning(string.Join(", ", productsResult.Warnings.Concat(categoriesResult.Warnings)));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Full synchronization failed");
            await transaction.RollbackAsync(CancellationToken.None);
            return SyncResult.Failure($"Synchronization failed: {ex.Message}");
        }
    }

    public async Task<SyncResult> SyncProductsAsync(CancellationToken ct = default)
    {
        try
        {

            var commandProducts = await _commandDb.Products
                .Where(p => !p.IsDeleted)
                .AsNoTracking()
                .ToListAsync(ct);


            var existingIds = new HashSet<long>(await _queryDb.Products
                .Select(p => p.Id)
                .ToListAsync(ct));

            int upserted = 0, deleted = 0;
            var warnings = new List<string>();

            foreach (var batch in commandProducts.Chunk(500))
            {
                foreach (var product in batch)
                {
                    if (existingIds.Contains(product.Id))
                        _queryDb.Entry(_queryDb.Products.Find(product.Id)!).CurrentValues.SetValues(product);

                    else
                        await _queryDb.Products.AddAsync(product, ct);

                    upserted++;
                }
            }


            var deletedIds = await _commandDb.Products
                .Where(p => p.IsDeleted)
                .Select(p => p.Id)
                .ToListAsync(ct);

            var toDelete = await _queryDb.Products
                .Where(p => deletedIds.Contains(p.Id))
                .ToListAsync(ct);

            _queryDb.Products.RemoveRange(toDelete);
            deleted = toDelete.Count;

            return SyncResult.Success(upserted, deleted);
        }

        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Product synchronization failed");
            return SyncResult.Failure($"Product sync failed: {ex.Message}");
        }
    }

    public async Task<SyncResult> SyncCategoriesAsync(CancellationToken ct = default)
    {
        try
        {

            var commandProducts = await _commandDb.ProductCategories
                .Where(p => !p.IsDeleted)
                .AsNoTracking()
                .ToListAsync(ct);


            var existingIds = new HashSet<int>(await _queryDb.ProductCategories
                .Select(p => p.Id)
                .ToListAsync(ct));

            int upserted = 0, deleted = 0;
            var warnings = new List<string>();


            foreach (var batch in commandProducts.Chunk(500))
            {
                foreach (var productCategory in batch)
                {
                    if (existingIds.Contains(productCategory.Id))
                    {
                        _queryDb.Entry(_queryDb.ProductCategories.Find(productCategory.Id)!).CurrentValues.SetValues(productCategory);
                    }
                    else
                    {
                        await _queryDb.ProductCategories.AddAsync(productCategory, ct);
                    }
                    upserted++;
                }
            }


            var deletedIds = await _commandDb.ProductCategories
                .Where(p => p.IsDeleted)
                .Select(p => p.Id)
                .ToListAsync(ct);

            var toDelete = await _queryDb.ProductCategories
                .Where(p => deletedIds.Contains(p.Id))
                .ToListAsync(ct);

            _queryDb.ProductCategories.RemoveRange(toDelete);
            deleted = toDelete.Count;

            return SyncResult.Success(upserted, deleted);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Product category synchronization failed");
            return SyncResult.Failure($"Product category sync failed: {ex.Message}");
        }
    }
}
