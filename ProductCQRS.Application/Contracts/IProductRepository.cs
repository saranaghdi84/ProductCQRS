using ProductCQRS.Domain.Entities;

namespace ProductCQRS.Application.Contracts;

public interface IProductRepository
{
    Task<bool> Activate(int productId);
    Task<bool> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(long ID);
}
