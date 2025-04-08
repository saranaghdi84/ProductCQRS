using ProductCQRS.Domain.Entities;

namespace ProductCQRS.Application.Contracts;

public interface IProductRepository
{
    Task<bool> Activate(int productId, CancellationToken cancellationToken);
    Task<bool> AddAsync(Product product, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(long ID);
}
