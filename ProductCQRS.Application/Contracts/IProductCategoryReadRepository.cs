using ProductCQRS.Domain.Entities;

namespace ProductCQRS.Application.Contracts;

public interface IProductCategoryReadRepository
{
    Task<ProductCategory> GetByIdAsync(int id , CancellationToken cancellationToken);
    Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken);

}
