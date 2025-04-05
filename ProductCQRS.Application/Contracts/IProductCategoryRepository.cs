using ProductCQRS.Domain.Entities;

namespace ProductCQRS.Application.Contracts;

public interface IProductCategoryRepository
{

    Task<bool> AddAsync(ProductCategory category);
    Task<bool> UpdateAsync(ProductCategory category);
    Task<bool> DeleteAsync(int category);
    Task<bool> HasProductsAsync(int categoryId);
}
