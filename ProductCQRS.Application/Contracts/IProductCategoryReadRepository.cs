using ProductCQRS.Domain.Entities;

namespace ProductCQRS.Application.Contracts;

public interface IProductCategoryReadRepository
{
    Task<ProductCategory> GetByIdAsync(int id);
    Task<IEnumerable<ProductCategory>> GetAllAsync();

}
