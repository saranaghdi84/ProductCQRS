namespace ProductCQRS.Application.Contracts;

public interface IReadUnitOfWork : IDisposable
{
    IProductReadRepository ProductReadRepository { get; }
    IProductCategoryReadRepository ProductCategoryReadRepository { get; }
}
