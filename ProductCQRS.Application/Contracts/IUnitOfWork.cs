namespace ProductCQRS.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    Task CommitAsync(CancellationToken cancellationToken);
}
