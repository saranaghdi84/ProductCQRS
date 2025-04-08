using ProductCQRS.Domain.Entities;
using System.Threading;

namespace ProductCQRS.Application.Contracts;

public interface IProductCategoryRepository
{

    Task<bool> AddAsync(ProductCategory category , CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ProductCategory category);
    Task<bool> DeleteAsync(int category);
    Task<bool> HasProductsAsync(int categoryId);
}
