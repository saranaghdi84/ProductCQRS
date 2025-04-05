using ProductCQRS.Domain.Entities;

namespace ProductCQRS.Application.Contracts;

public interface IProductReadRepository
{
    Task<Product> GetByIdAsync(long id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

}
